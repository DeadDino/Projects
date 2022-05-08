using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EKRLib
{
    public class MotorBoat : Transport
    {
        /// <summary>
        /// Переписанный метод звука.
        /// </summary>
        /// <returns></returns>
        public override string StartEngine()
        {
            return $"<{model}>:Brrrbrr";
        }
        /// <summary>
        /// Конструктор лодок.
        /// </summary>
        /// <param name="model">модель</param>
        /// <param name="power">мощность</param>
        public MotorBoat(string model, uint power) : base(model, power)
        {

        }
        /// <summary>
        /// Переопределенный метод ToString.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {

            return "MotorBoat. " + base.ToString();
            
        }
    }
}
