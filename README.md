# MetalAnalyzer

Программа предназначена для анализа снимков шлифов сплавов путём определения коэффициентов форм каждой отдельной микрочастицы. После чего
выполеняется построение гистограмного распредления, отображающего какое количество микрочастиц каждого класса присутствует на исходном
снимке, что позволит сделать дальнейшие выводы о прочностных характеристиках анализируемого образца.
Визуальный интерфейс достаточно приметивен и полностью написан с 0, за исключением анимированного окна гистограммного распределения Chart.

Инструкция по использованию:
1) Выбрать исходное изображение (тестовые снимки находятся в папке View/TestImages/)
2) Задать исходные параметры:
   - критерий очистки задаёт минимальное количество пикселей микрочастиц (убирает пыль, грязь);
   - черное-белое задаёт режим работы в черно-белых тонах;
   - цветовая чувствительность - задаёт допустимый разброс оттенков при работе алгоритма относительный созданной выборки фаз (чем больше 
   выборка оттенков и меньше значение цветовой чувствительности, тем точнее результат);
3) Создать выборку фаз. Создание происходит путём нажатия правой кнопки мыши по исходному изображению, причём, существует 2 события:
  а) В табличной части выбрана фаза - добавление оттенков в выбранную фазу;
  б) В табличной части не выбрана фаза (поле "Выбранные фазы" неактивно) - создание новой фазы с выбранным оттенком.
4) Выполнить обработку.

После выполнения алгоритма сформируется обработанное изображение, на котором можно выделить микрочастицы определённого класса при помощи 
нижней панели "Классы". Кроме того, станут стоступны 2 новые вкладки: "Микрочастицы" и "Аналитика". В них представлен результат 
работы алгоритма, в частности, детальная информация по каждой отдельной микрочастице, а также итоговое гистограммное распределение.