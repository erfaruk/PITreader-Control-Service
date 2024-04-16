using ReaderWorker.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderWorker.Services
{
    public class Readers
    {
        public List<Pitreader> AccesControl()
        {
            try
            {
                using var context = new ReaderExpertContext();
                var readers = context.Pitreaders.ToList();
                if (readers != null)
                {
                    return readers;
                }
                else
                {
                    return new List<Pitreader>();
                }
            }
            catch
            {
                throw;
            }

        }
    }
}
