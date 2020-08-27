using System.Collections.Generic;
using LancheLunch.Models;

namespace LancheLunch.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Lanche> LanchesPreferidos { get; set; }
    }
}
