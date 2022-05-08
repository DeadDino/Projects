using System;

namespace EKRLib
{
    public abstract class Transport
    {
        
        // строковая переменная, отвечающая за название модели.
        public string model;

        /// <summary>
        /// Строковое свойство Model, которое описывает название транспорта.
        /// </summary>
        public string Model
        {
            get
            {
                return model;
            }
            set
            {   
                //проверям название на корректность, согласно условию.
                int flag = 0;
                for (int i = 0; i < value.Length; i++)
                {
                    if (((value[i] >= 'A') && (value[i] <= 'Z')) || (value[i] >= '0' && value[i] <= '9'))
                        flag++;
                }
                    if (value.Length !=5 ||  flag != 5)
                {
                    throw new TransportException($"Недопустимая модель <{value}> ") ;
                }
                else
                {
                    model = value;
                }
                    

            }
        }
        //числовая переменная, отвечающая за мощность.
        public uint power;
        public uint Power
        {
            get
            {
                return power;
            }
            set
            {
                //проверям мощность на корректность, согласно условию.
                if (value < 20)
                    throw new TransportException("мощность не может быть меньше 20 л.с.");
                else
                    power = value;
            }
        }
        /// <summary>
        /// Переопределенный метод ToString.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {

            string z = $"Model: <{model}>, Power: <{power}> ";
            return z;
        }
        public abstract string StartEngine();
        
        /// <summary>
        /// Конструктор класса транспорт.
        /// </summary>
        /// <param name="model">модель</param>
        /// <param name="power">мощность</param>
        public Transport(string model, uint power)
        {
            this.model = model;
            this.power = power;
        }
    }
}
