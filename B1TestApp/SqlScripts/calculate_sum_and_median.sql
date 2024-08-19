CREATE OR REPLACE FUNCTION calculate_sum_and_median(in_table_name TEXT)
RETURNS TABLE (column_name TEXT, integer_sum BIGINT, numeric_median NUMERIC) AS $$
DECLARE
    column_record RECORD;
    column_type TEXT;
    sql_query TEXT;
BEGIN
    FOR column_record IN
        SELECT c.column_name, c.data_type
        FROM information_schema.columns c
        WHERE c.table_name = in_table_name
          AND c.column_name NOT IN (
              SELECT kc.column_name
              FROM information_schema.table_constraints tc
              JOIN information_schema.key_column_usage kc
              ON tc.constraint_name = kc.constraint_name
              WHERE tc.table_name = in_table_name
                AND tc.constraint_type = 'PRIMARY KEY'
          )
    LOOP
        column_type := column_record.data_type;

        IF column_type IN ('integer', 'bigint', 'smallint') THEN
            sql_query := format(
                'SELECT 
                    %L AS column_name,
                    COALESCE(SUM(%I)::BIGINT, 0) AS integer_sum,
                    NULL::NUMERIC AS numeric_median
                 FROM %I',
                column_record.column_name,
                column_record.column_name,
                in_table_name
            );
            RETURN QUERY EXECUTE sql_query;

        ELSIF column_type IN ('decimal', 'numeric', 'real', 'double precision') THEN
            sql_query := format(
                'SELECT 
                    %L AS column_name,
                    NULL::BIGINT AS integer_sum,
                    COALESCE(percentile_cont(0.5) WITHIN GROUP (ORDER BY %I)::NUMERIC, 0) AS numeric_median
                 FROM %I',
                column_record.column_name,
                column_record.column_name,
                in_table_name
            );
            RETURN QUERY EXECUTE sql_query;
        END IF;
    END LOOP;
END;
$$ LANGUAGE plpgsql;
