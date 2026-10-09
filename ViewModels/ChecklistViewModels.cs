using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MPI_Report.ViewModels
{
    public class ChecklistItemViewModel
    {
        public int ChecklistItemId { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        public string Result { get; set; }
    }

    public class ChecklistListItemViewModel
    {
        public int ChecklistId { get; set; }

        public string Title { get; set; }

        public string Frequency { get; set; }

        public DateTime ChecklistDate { get; set; }

        public int ItemCount { get; set; }

        public int CompletedCount { get; set; }

        public int CompletionPercent { get; set; }

        public IList<ChecklistItemViewModel> Items { get; set; }

        public ChecklistListItemViewModel()
        {
            Items = new List<ChecklistItemViewModel>();
        }
    }

    public class ChecklistFormViewModel
    {
        public int ChecklistId { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [StringLength(50)]
        public string Frequency { get; set; }

        public DateTime ChecklistDate { get; set; }

        public IList<string> ItemLines { get; set; }

        public ChecklistFormViewModel()
        {
            ItemLines = new List<string>();
            Frequency = "Daily";
            ChecklistDate = DateTime.Today;
        }
    }

    public class ChecklistToggleItemViewModel
    {
        [Required]
        public int ChecklistItemId { get; set; }

        public bool IsCompleted { get; set; }

        [StringLength(50)]
        public string Result { get; set; }
    }
}
