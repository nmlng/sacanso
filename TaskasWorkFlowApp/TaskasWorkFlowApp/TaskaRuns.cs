//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TaskasWorkFlowApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class TaskaRuns
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaskaRuns()
        {
            this.NoteRuns = new HashSet<NoteRuns>();
            this.ParameterRuns = new HashSet<ParameterRuns>();
            this.TaskaRunChilds = new HashSet<TaskaRunChilds>();
            this.TaskaRunChilds1 = new HashSet<TaskaRunChilds>();
        }
    
        public int TaskaRunId { get; set; }
        public string TaskaRunName { get; set; }
        public int Status { get; set; }
        public int TaskaId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NoteRuns> NoteRuns { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ParameterRuns> ParameterRuns { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaskaRunChilds> TaskaRunChilds { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaskaRunChilds> TaskaRunChilds1 { get; set; }
        public virtual Taskas Taskas { get; set; }
    }
}
