namespace Home5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GameResult
    {
        public int Id { get; set; }

        public string Result { get; set; }

        public string FirstPlayer { get; set; }

        public string SecondPlayer { get; set; }

        public int? StepsCount { get; set; }

        public DateTime? GameTime { get; set; }
    }
}
