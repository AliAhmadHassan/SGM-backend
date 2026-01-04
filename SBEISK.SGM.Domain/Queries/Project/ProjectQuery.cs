namespace SBEISK.SGM.Domain.Queries.Project
{
    public class ProjectQuery 
    {
        public int? Code { get; set; }
        public string Description { get; set; }
        public string Initials { get; set; }
        public bool? Active { get; set; }
        public string BranchOffice { get; set; }
    }
}