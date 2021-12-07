using System;

namespace Lab2.MatrixCalculator
{
    public class MatrixCalculator
    {
        private int[,] result_m;
        private int[,] m;
        private int? number;
        private char operation = '\0'; // для действия
        private bool operation_transpose;
        private bool last_input_is_not_operation = true; // Последний ввод это операция

        public string[,] Execute(string value)
        {
            const string error = "E";

            // Если вызвано транспонирование
            if (value == "TR")
            {
                //if (!last_input_is_not_operation)
                //    return error; // если до этого было введено число или массив (а также и ничего)

                operation_transpose = true;
                Console.Write("\n\ttranspose");
                return MatrixHandler.ConvertMatrixIntToStr(result_m);
            }
            
            // Послдений ввод это не операция
            if (last_input_is_not_operation)
            {
                // Попытка преобразовтаь в число
                if (int.TryParse(value, out int num))
                {
                    if (number != null)
                        return MatrixHandler.ConvertStrToMatrixStr(error);

                    number = num;
                    last_input_is_not_operation = true;
                    return MatrixHandler.ConvertStrToMatrixStr(number.ToString());
                }
            }

            // Послдений ввод это не операция либо операция транспонирования
            if (last_input_is_not_operation || operation_transpose)
            {
                // Попытка взять как матрицу
                if (MatrixHandler.TryParseToMatrix(value, out int[,] tmp_m))
                {
                    if (result_m == null)
                    {
                        result_m = tmp_m;
                        last_input_is_not_operation = true;
                        return MatrixHandler.ConvertMatrixIntToStr(result_m);
                    }

                    if (m == null)
                    {
                        m = tmp_m;
                        last_input_is_not_operation = true;
                        return MatrixHandler.ConvertMatrixIntToStr(m);
                    }
                    
                    return MatrixHandler.ConvertStrToMatrixStr(error);
                }
            }

            // Проверка на допустимость операции
            if (value is "+" or "*" or "=")
            {
                if (!last_input_is_not_operation)
                    return MatrixHandler.ConvertStrToMatrixStr(error);

                // выполнить хранимую оперцию
                switch (operation)
                {
                    case '\0':
                        // Отсутствие актуальных операций
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
                return MatrixHandler.ConvertMatrixIntToStr(result_m);
            }

            return MatrixHandler.ConvertStrToMatrixStr(error);
        }
    }
}