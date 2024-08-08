using Data.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RejestratorLogowaniaService
    {
        private readonly RejestratorLogowaniaRepository rejestratorLogowaniaRepository;

        public RejestratorLogowaniaService(RejestratorLogowaniaRepository rejestratorLogowaniaRepository)
        {
            this.rejestratorLogowaniaRepository = rejestratorLogowaniaRepository;
        }
    }
}
