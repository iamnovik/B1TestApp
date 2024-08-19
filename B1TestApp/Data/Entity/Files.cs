using System.Collections.Generic;

namespace B1TestApp.Data.Entity;

public class Files
{
    public long Id { get; set; }
    
    public string Name { get; set; }

    public virtual IEnumerable<Bank> Banks { get; set; }
}