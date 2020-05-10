using System.Collections.Generic;

namespace MCJPortal.ViewModels.ViewModels
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ProjectLineViewModel> ProjectLines { get; set; }
    }
}