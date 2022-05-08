using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EKRLib
{
    public class Car : Transport
    {
        
        /// <summary>
        /// Переписанный метод звука.
        /// </summary>
        /// <returns></returns>
        public override string StartEngine()
        {
            return $"<{model}>:Vroom";
        }
        /// <summary>
        /// Конструктор машин.
        /// </summary>
        /// <param name="model">модель</param>
        /// <param name="power">мощность</param>
        public Car(string model, uint power) : base(model, power)
        {

        }
        /// <summary>
        /// Переопределенный метод ToString.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Car. " + base.ToString();

        }

    }
}
