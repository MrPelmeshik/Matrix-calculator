using System;

namespace Lab2.MatrixCalculator
{
    public class MatrixCalculator
    {
        private const char _number = 'n';
        private const char _matrix = 'm';
        private const char _operation = 'o';
        private const char _equal = '=';
        
        private char last_input = '\0';
        private int[,] result_m;
        private int[,] m;
        private int? number;
        private char operation = '\0'; // для действия

        public string[,] Execute(string value)
        {
            const string error = "E";

            // Если вызвано транспонирование
            if (value == "TR")
            {
                if (last_input is _matrix or _equal)
                {
                    if (m != null)
                    {
                        m = MatrixHandler.Transpose(m);
                        return MatrixHandler.ConvertMatrixIntToStr(m);
                    } 
                    
                    if (result_m != null)
                    {
                        result_m = MatrixHandler.Transpose(result_m);
                        return MatrixHandler.ConvertMatrixIntToStr(result_m);
                    }
                }
                Console.Write("\nErrrrror");
                return MatrixHandler.ConvertStrToMatrixStr(error);
            }

            // Послдений ввод это не операция
            if (last_input is not (_equal and _operation))
            {
                // Попытка преобразовтаь в число
                if (int.TryParse(value, out int num))
                {
                    if (number != null)
                        return MatrixHandler.ConvertStrToMatrixStr(error);

                    number = num;
                    last_input = _number;
                    return MatrixHandler.ConvertStrToMatrixStr(number.ToString());
                }
            }
            
            // Послдений ввод это не операция либо операция транспонирования
            if (last_input is not (_equal and _operation))
            {
                // Попытка взять как матрицу
                if (MatrixHandler.TryParseToMatrix(value, out int[,] tmp_m))
                {
                    if (result_m == null)
                    {
                        result_m = tmp_m;
                        last_input = _matrix;
                        return MatrixHandler.ConvertMatrixIntToStr(result_m);
                    }

                    if (m == null)
                    {
                        m = tmp_m;
                        last_input = _matrix;
                        return MatrixHandler.ConvertMatrixIntToStr(m);
                    }
                    return MatrixHandler.ConvertStrToMatrixStr(error);
                }
            }

            // Проверка на допустимость операции
            if (value is "+" or "*" or "=")
            {
                if (last_input == _operation)
                    return MatrixHandler.ConvertStrToMatrixStr(error);
                
                // выполнить хранимую оперцию
                switch (operation)
                {
                    case '\0' or _equal:
                        // Отсутствие актуальных операций или равенство
                        break;
                    case '*':
                        if (number != null)
                        {
                            result_m = MatrixHandler.MulMatrixByNumber(
                                result_m != null ? result_m : m,
                                number.Value);
                            number = null;
                            m = null;
                        }
                        else if (m != null && result_m != null)
                        {
                            result_m = MatrixHandler.MulMatrix(result_m, m);
                            m = null;
                        }
                        else
                        {
                            return MatrixHandler.ConvertStrToMatrixStr(error);
                        }

                        break;
                    case '+':
                        if (m != null && result_m != null)
                        {
                            result_m = MatrixHandler.SumMatrix(result_m, m);
                            m = null;
                        }
                        else
                        {
                            return MatrixHandler.ConvertStrToMatrixStr(error);
                        }

                        break;
                    default:
                        return MatrixHandler.ConvertStrToMatrixStr(error);
                }

                operation = value[0];
                last_input = value is "+" or "*" ? _operation : _equal;
                return MatrixHandler.ConvertMatrixIntToStr(result_m);
            }
            
            return MatrixHandler.ConvertStrToMatrixStr(error);
        }
    }
}