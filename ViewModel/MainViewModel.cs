using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Model;
using ViewModel.Annotations;
using LiveCharts;
using LiveCharts.Wpf;
using System.Timers;
using Timer = System.Timers.Timer;
using Excel = Microsoft.Office.Interop.Excel;

namespace ViewModel
{
    public class MainViewModel :INotifyPropertyChanged
    {
        private bool _isDetailsVisibility;
        private bool _isMainTabSelected;
        private bool _isStatusBarVisible;
        private bool _isBlackAndWhite;
        private bool[,] _blockPixels;


        private int _minimumCountOfPixels;
        private double _statusBarValue;
        private double _colorSensitivity;

        private string _file;
        private string _newFile;
        private string _statusMessage;
        private string _myTimer;
        private string[] _labels;

        private Bitmap _sourceImage;
        private Bitmap _processedImage;
        private Phase _selectedPhase;
        private Microparticle _selectedMicroparticle;
        private Class _selectedClasseOfMicroparticles;

        private List<Microparticle> _microparticles;
        private List<Pixel> _temPixelsMassive;
        private List<Class> _classesOfMicroparticles;
        private ObservableCollection<Phase> _phases;
        private SeriesCollection _seriesCollection;
        
        private readonly BackgroundWorker _worker;
        public DateTime StartDateTime;

        private ICommand _processOfPhasesCommand;

        public bool IsDetailsVisibility
        {
            get => _isDetailsVisibility;
            set
            {
                if (value == _isDetailsVisibility) return;
                _isDetailsVisibility = value;
                OnPropertyChanged();
            }
        }

        public bool IsMainTabSelected
        {
            get => _isMainTabSelected;
            set
            {
                if (value == _isMainTabSelected) return;
                _isMainTabSelected = value;
                OnPropertyChanged();
            }
        }

        public string File
        {
            get => _file;
            set
            {
                if (value == _file) return;
                _file = value;
                OnPropertyChanged();
            }
        }

        public Bitmap SourceImage
        {
            get => _sourceImage;
            set
            {
                if (Equals(value, _sourceImage)) return;
                _sourceImage = value;
                OnPropertyChanged();
            }
        }
        public Bitmap ProcessedImage
        {
            get => _processedImage;
            set
            {
                if (Equals(value, _processedImage)) return;
                _processedImage = value;
                OnPropertyChanged();
            }
        }

        public string NewFile
        {
            get => _newFile;
            set
            {
                if (value == _newFile) return;
                _newFile = value;
                OnPropertyChanged();
            }
        }
      

        public Microparticle SelectedMicroparticle
        {
            get => _selectedMicroparticle;
            set
            {
                if (value == _selectedMicroparticle) return;
                _selectedMicroparticle = value;
                OnPropertyChanged();
            }
        }

        public double StatusBarValue
        {
            get => _statusBarValue;
            set
            {
                if (value.Equals(_statusBarValue)) return;
                _statusBarValue = value;
                OnPropertyChanged();
            }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                if (value == _statusMessage) return;
                _statusMessage = value;
                OnPropertyChanged();
            }
        }

        public bool IsStatusBarVisible
        {
            get => _isStatusBarVisible;
            set
            {
                if (value == _isStatusBarVisible) return;
                _isStatusBarVisible = value;
                OnPropertyChanged();
            }
        }

        public bool IsBlackAndWhite
        {
            get => _isBlackAndWhite;
            set
            {
                if (value == _isBlackAndWhite) return;
                _isBlackAndWhite = value;
                OnPropertyChanged();
            }
        }

        public double ColorSensitivity
        {
            get => _colorSensitivity;
            set
            {
                if (value.Equals(_colorSensitivity)) return;
                _colorSensitivity = value;
                OnPropertyChanged();
            }
        }

        public int MinimumCountOfPixels
        {
            get => _minimumCountOfPixels;
            set
            {
                if (value == _minimumCountOfPixels) return;
                _minimumCountOfPixels = value;
                OnPropertyChanged();
            }
        }

        public List<Microparticle> Microparticles
        {
            get => _microparticles;
            set
            {
                if (Equals(value, _microparticles)) return;
                _microparticles = value;
                OnPropertyChanged();
            }
        }

        public bool[,] BlockPixels
        {
            get => _blockPixels;
            set
            {
                if (Equals(value, _blockPixels)) return;
                _blockPixels = value;
                OnPropertyChanged();
            }
        }

        public List<Pixel> TemPixelsMassive
        {
            get => _temPixelsMassive;
            set
            {
                if (Equals(value, _temPixelsMassive)) return;
                _temPixelsMassive = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Phase> Phases
        {
            get => _phases ?? (_phases = new ObservableCollection<Phase>());
            set
            {
                if (Equals(value, _phases)) return;
                _phases = value;
                OnPropertyChanged();
            }
        }

        public SeriesCollection SeriesCollection
        {
            get => _seriesCollection ?? (_seriesCollection = new SeriesCollection());
            set
            {
                if (Equals(value, _seriesCollection)) return;
                _seriesCollection = value;
                OnPropertyChanged();
            }
        }

        public string[] Labels
        {
            get => _labels ?? (_labels = new string[0]);
            set
            {
                if (Equals(value, _labels)) return;
                _labels = value;
                OnPropertyChanged();
            }
        }

        public List<Class> ClassesOfMicroparticles
        {
            get => _classesOfMicroparticles;
            set
            {
                if (Equals(value, _classesOfMicroparticles)) return;
                _classesOfMicroparticles = value;
                OnPropertyChanged();
            }
        }

        public Class SelectedClasseOfMicroparticles
        {
            get => _selectedClasseOfMicroparticles;
            set
            {
                if (value.Equals(_selectedClasseOfMicroparticles)) return;
                _selectedClasseOfMicroparticles = value;
                OnPropertyChanged();
            }
        }


        public Phase SelectedPhase
        {
            get => _selectedPhase;
            set
            {
                if (Equals(value, _selectedPhase)) return;
                _selectedPhase = value;
                OnPropertyChanged();
            }
        }

        public String MyTimer
        {
            get => _myTimer;
            set
            {
                if (value == _myTimer) return;
                _myTimer = value;
                OnPropertyChanged();
            }
        }

        public ICommand ChageImageCommand { get; }
        public ICommand SelectClassForMicroparticlesCommand { get; }
        public ICommand ApplicationExitCommand { get; }
        public ICommand InstigateWorkCommand { get; }
        public ICommand SaveResultsCommand { get; }
        public ICommand ProcessOfPhasesCommand
        {
            get
            {
                return _processOfPhasesCommand ?? (_processOfPhasesCommand =
                           new DelegateCommand(param => ProcessOfPhases((MouseEventArgs)param)));
            }
            set
            {
                if (Equals(value, _processOfPhasesCommand)) return;
                _processOfPhasesCommand = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            SelectClassForMicroparticlesCommand = new DelegateCommand(SelectClassForMicroparticles);
            ApplicationExitCommand = new DelegateCommand(ExitFromApplication);
            SaveResultsCommand = new DelegateCommand(SaveResults);
            ChageImageCommand = new DelegateCommand(ChangeImage);
            InstigateWorkCommand =
                new DelegateCommand(o => _worker.RunWorkerAsync(),
                    o => !_worker.IsBusy);
            _worker = new BackgroundWorker();
            _worker.DoWork += PerformImage;

            ClassesOfMicroparticles = new List<Class>
            {
                new Class
                {
                    Number = 1,
                    Min = 0,
                    Max = 1.5,
                    Image = Resource1._1class
                }
            };
            for (var i = 2; i < 10; i++)
            {
                Bitmap img = (Bitmap)Resource1.ResourceManager.GetObject("_" + i + "class");
                ClassesOfMicroparticles.Add(new Class
                {
                    Number = i,
                    Min = i - 0.5,
                    Max = i + 0.5,
                    Image = img
                });
            }
            ClassesOfMicroparticles.Add(new Class
            {
                Number = 10,
                Min = 9.5,
                Max = 10,
                Image = Resource1._10class
            });
            ColorSensitivity = 100;
            IsStatusBarVisible = false;
            IsDetailsVisibility = false;
            IsMainTabSelected = true;
        }

        public void SelectClassForMicroparticles(object obj)
        {

            if (!(obj is bool isSelect) || ProcessedImage == null)
            {
                MessageBox.Show("Сначала обработайте исходное изображение!", "Внимание");
                return;
            }
            var tempImage = InitializeStartImage();
            if (isSelect)
            {
                var microparticles = Microparticles.Where(m => m.ClassOfMicroparticle == SelectedClasseOfMicroparticles).ToList();
                foreach (var microparticle in microparticles)
                {
                    foreach (var pixel in microparticle.Border)
                    {
                        tempImage.SetPixel(pixel.X1, pixel.X2, Color.Red);
                    }
                }
            }
            ProcessedImage = tempImage;
        }

        public Bitmap InitializeStartImage()
        {
            var tempImage = new Bitmap(ProcessedImage, ProcessedImage.Width, ProcessedImage.Height);
            foreach (var microparticle in Microparticles)
            {
                foreach (var pixel in microparticle.Border)
                {
                    tempImage.SetPixel(pixel.X1, pixel.X2, pixel.Color);
                }
            }
            return tempImage;
        }

        private void ProcessOfPhases(MouseEventArgs e)
        {
            var p = e.GetPosition((IInputElement)e.Source);
            var widthcoef = ((System.Windows.Controls.Image)e.Source).ActualWidth / SourceImage.Width;
            var heightcoef = ((System.Windows.Controls.Image)e.Source).ActualHeight / SourceImage.Height;
            if (SelectedPhase == null)
            {
                Phases.Add(new Phase
                {
                    Number = (Phases.Count + 1),
                    Name = "Фаза " + (Phases.Count + 1),
                    Colors = new ObservableCollection<Color> { SourceImage.GetPixel((int)(p.X / widthcoef), (int)(p.Y / heightcoef)) }
                });
            }
            else
            {
                SelectedPhase.Colors.Add(SourceImage.GetPixel((int)(p.X / widthcoef), (int)(p.Y / heightcoef)));
            }

        }

        private void ExitFromApplication(object o1)
        {
            Environment.Exit(0);
        }

        private void SaveResults(object o1)
        {
            if (Microparticles != null && Microparticles.Count > 0 && ClassesOfMicroparticles != null && ClassesOfMicroparticles.Count > 0 && !String.IsNullOrEmpty(File))
            {
                var excelapp = new Excel.Application();
                var workBook = excelapp.Workbooks.Add();
                var workSheet = (Excel.Worksheet)workBook.Worksheets.Item[1];
                workSheet.Cells[1, 1] = "Микрочастица";
                workSheet.Cells[1, 2] = "Периметр";
                workSheet.Cells[1, 3] = "Площадь";
                workSheet.Cells[1, 4] = "R";
                workSheet.Cells[1, 5] = "D";
                workSheet.Cells[1, 6] = "Фактор формы";
                workSheet.Cells[1, 7] = "Класс";
                var count = 2;
                foreach (var microparticle in Microparticles)
                {
                    if (microparticle.Coefficient <= 1)
                    {
                        workSheet.Cells[count, 1] = "Микрочастица №" + (count - 1);
                        workSheet.Cells[count, 2] = microparticle.L;
                        workSheet.Cells[count, 3] = microparticle.F;
                        workSheet.Cells[count, 4] = microparticle.R;
                        workSheet.Cells[count, 5] = microparticle.D;
                        workSheet.Cells[count, 6] = microparticle.Coefficient;
                        try
                        {
                            workSheet.Cells[count, 7] = microparticle.ClassOfMicroparticle.Number;
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                        count++;
                    }
                }

                workSheet.Cells[1, 10] = "Класс";
                workSheet.Cells[1, 11] = "Количество";
                count = 2;
                foreach (var classmicroparticle in ClassesOfMicroparticles)
                {
                    workSheet.Cells[count, 10] = classmicroparticle.Number;
                    workSheet.Cells[count, 11] = classmicroparticle.Count;
                    count++;
                }
                Excel.ChartObjects chartObjs = (Excel.ChartObjects)workSheet.ChartObjects();
                Excel.ChartObject chartObj = chartObjs.Add(5, 50, 300, 300);
                Excel.Chart xlChart = chartObj.Chart;
                Excel.Range rng2 = workSheet.Range["K1:K11"];
                xlChart.ChartType = Excel.XlChartType.xlConeBarStacked;
                xlChart.SetSourceData(rng2);
                workSheet.Columns.EntireColumn.AutoFit();
                workSheet.Columns.EntireRow.AutoFit();
                workSheet.Range["A1", "Z1"].Font.Bold = true;
                workSheet.Columns.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                excelapp.Visible = true;
                excelapp.UserControl = true;
            }
            else
            {
                MessageBox.Show("Перед сохранением выполните обработку изображения!", "Внимание");
            }
          
        }
        
        private void ChangeImage(object obj)
        {
            StatusBarValue = 0;
            var dlg = new OpenFileDialog
            {
                DefaultExt = ".jpeg",
                Filter = "Images (*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG"
            };

            var result = dlg.ShowDialog();

            if (result != true) return;
            File = dlg.FileName;
            SourceImage = new Bitmap(File);
            NewFile = null;
            Phases = new ObservableCollection<Phase>();
            IsDetailsVisibility = false;
            FullReset();
            ProcessedImage = null;
        }

        public void FullReset()
        {
            StatusBarValue = 0;
            NewFile = null;
            IsMainTabSelected = true;
            SelectedMicroparticle = new Microparticle();
            Microparticles = new List<Microparticle>();
            SeriesCollection = new SeriesCollection();
            foreach (var classesOfMicroparticle in ClassesOfMicroparticles)
            {
                classesOfMicroparticle.Count = 0;
            }
        }

      private void PerformImage(object obj, DoWorkEventArgs e)
        {
            if (string.IsNullOrEmpty(File))
            {
                MessageBox.Show("Сначала выберите исходное изображение!", "Внимание");
                return;
            }
            if (Phases.Count == 0)
            {
                MessageBox.Show("Сначала создайте выборку фаз!", "Внимание");
                return;
            }
            FullReset();
            IsStatusBarVisible = true;
            IsDetailsVisibility = false;
            StartDateTime = DateTime.Now;
            
            var timer = new Timer();
            timer.Elapsed += ChangeTimer;
            timer.Start();

             var newImage = new Bitmap(SourceImage.Width, SourceImage.Height);

            var backgroundColor = Color.FromArgb(255,255,255);
            const int countOfStages = 3;
            var numberOfStage = 1;

            StatusMessage = "Этап " + numberOfStage + " из " + countOfStages + ". Обработка фаз/цветов.";
            for (var i = 0; i < newImage.Width; i++)
            {
                for (var j = 0; j < newImage.Height; j++)
                {
                    var pixel = SourceImage.GetPixel(i, j);
                    if (IsBlackAndWhite)
                    {
                        var r = (float)((pixel.ToArgb() & 0x00FF0000) >> 16);
                        var g = (float)((pixel.ToArgb() & 0x0000FF00) >> 8);
                        var b = (float)(pixel.ToArgb() & 0x000000FF);
                        r = g = b = (r + g + b) / 3.0f;
                        var newPixel = 0xFF000000 | ((uint)r << 16) | ((uint)g << 8) | ((uint)b);
                        pixel = Color.FromArgb((int) newPixel);
                    }
                    newImage.SetPixel(i, j, GetPhasesForPixel(pixel) != null ? pixel : backgroundColor);
                }
            }
            numberOfStage++;
            BlockPixels = new bool[newImage.Width, newImage.Height];
            StatusMessage = "Этап " + numberOfStage + " из " + countOfStages + ". Дробление изображения.";
            var numberMicroparticle = 1;
            for (var i = 0; i < newImage.Width; i++)
            {
                for (var j = 0; j < newImage.Height; j++)
                {
                    if (BlockPixels[i, j]) continue;
                    var pixel = newImage.GetPixel(i, j);
                    var color = Color.FromArgb(pixel.R, pixel.G, pixel.B);
                    var addedPixel = new Pixel
                    {
                        X1 = i,
                        X2 = j,
                        Color = color
                    };
                    if (color == backgroundColor) continue;

                    var newMicroparticles = new List<Microparticle>();

                    var microparticlePhases = GetPhasesForPixel(addedPixel.Color);

                    Microparticle microparticle;
                    foreach (var microparticlePhase in microparticlePhases)
                    {
                        microparticle = new Microparticle
                        {
                            Number = numberMicroparticle,
                            Pixels = new List<Pixel>(),
                            MinX1 = i,
                            Phase = microparticlePhase
                        };
                        microparticle.Pixels.Add(addedPixel);
                        BlockPixels[addedPixel.X1, addedPixel.X2] = true;
                        TemPixelsMassive = new List<Pixel> {addedPixel};
                        while (true)
                        {
                            var selectedPixel = FindNextPixel(newImage, backgroundColor, microparticle.Phase ?? new Phase());
                            if (selectedPixel == null)
                            {
                                break;
                            }
                            microparticle.Pixels.Add(selectedPixel);
                            BlockPixels[selectedPixel.X1, selectedPixel.X2] = true;
                            TemPixelsMassive.Add(selectedPixel);
                        }
                        newMicroparticles.Add(microparticle);
                        foreach (var microparticlePixel in microparticle.Pixels)
                        {
                            BlockPixels[microparticlePixel.X1, microparticlePixel.X2] = false;
                        }
                    }

                    microparticle = newMicroparticles.OrderByDescending(m => m.Pixels.Count).ToList()[0];
                    foreach (var mic in microparticle.Pixels)
                    {
                        BlockPixels[mic.X1, mic.X2] = true;
                    }
                    if (microparticle.Pixels.Count < MinimumCountOfPixels || microparticle.Pixels.Count < 3)
                    {
                        foreach (var microparticlePixel in microparticle.Pixels)
                        {
                            newImage.SetPixel(microparticlePixel.X1, microparticlePixel.X2, backgroundColor);
                        }
                        continue;
                    }
                    Microparticles.Add(microparticle);
                    numberMicroparticle++;
                }
            }

            numberOfStage++;

            StatusMessage = "Этап " + numberOfStage + " из " + countOfStages + ". Обработка микрочастиц.";

            foreach (var microparticle in Microparticles)
            {
                microparticle.MinX1 = microparticle.Pixels[0].X1;
                microparticle.MinX2 = microparticle.Pixels[0].X2;
                microparticle.MaxX1 = microparticle.Pixels[0].X1;
                microparticle.MaxX2 = microparticle.Pixels[0].X2;
                foreach (var i in microparticle.Pixels)
                {
                    if (i.X1 < microparticle.MinX1)
                    {
                        microparticle.MinX1 = i.X1;
                    }
                    if (i.X2 < microparticle.MinX2)
                    {
                        microparticle.MinX2 = i.X2;
                    }
                    if (i.X1 > microparticle.MaxX1)
                    {
                        microparticle.MaxX1 = i.X1;
                    }
                    if (i.X2 > microparticle.MaxX2)
                    {
                        microparticle.MaxX2 = i.X2;
                    }
                }
                microparticle.Image = new Bitmap(microparticle.MaxX1 - microparticle.MinX1 + 1, microparticle.MaxX2 - microparticle.MinX2 + 1);
                for (var i = 0; i < microparticle.Image.Width; i++)
                {
                    for (var j = 0; j < microparticle.Image.Height; j++)
                    {
                        microparticle.Image.SetPixel(i,j, backgroundColor);
                    }
                }
                foreach (var microparticlePixel in microparticle.Pixels)
                {
                    microparticle.Image.SetPixel(microparticlePixel.X1 - microparticle.MinX1, microparticlePixel.X2 - microparticle.MinX2, microparticlePixel.Color);
                }

                microparticle.R =  microparticle.MaxX1 - microparticle.MinX1 <
                                   (microparticle.MaxX2 - microparticle.MinX2) ?
                    (double)1 / (microparticle.MaxX2 - microparticle.MinX2 + 1) : (double)1 / (microparticle.MaxX1 - microparticle.MinX1 + 1);

                microparticle.Border = new List<Pixel>();
                foreach (var microparticlePixel in microparticle.Pixels)
                {
                    Color color1;
                    Color color2;
                    Color color3;
                    Color color4;
                    try
                    {
                        color1 = microparticle.Image.GetPixel(microparticlePixel.X1 - microparticle.MinX1 - 1, microparticlePixel.X2 - microparticle.MinX2);
                    }
                    catch (Exception)
                    {
                        color1 = backgroundColor;
                    }
                    if (color1 == backgroundColor)
                    {
                        microparticlePixel.PixelBorderEdges++;
                    }
                    try
                    {
                        color2 = microparticle.Image.GetPixel(microparticlePixel.X1 - microparticle.MinX1 + 1, microparticlePixel.X2 - microparticle.MinX2);
                    }
                    catch (Exception)
                    {
                        color2 = backgroundColor;
                    }
                    if (color2 == backgroundColor)
                    {
                        microparticlePixel.PixelBorderEdges++;
                    }
                    try
                    {
                        color3 = microparticle.Image.GetPixel(microparticlePixel.X1 - microparticle.MinX1, microparticlePixel.X2 - microparticle.MinX2 - 1);
                    }
                    catch (Exception)
                    {
                        color3 = backgroundColor;
                    }
                    if (color3 == backgroundColor)
                    {
                        microparticlePixel.PixelBorderEdges++;
                    }
                    try
                    {
                        color4 = microparticle.Image.GetPixel(microparticlePixel.X1 - microparticle.MinX1, microparticlePixel.X2 - microparticle.MinX2 + 1);
                    }
                    catch (Exception)
                    {
                        color4 = backgroundColor;
                    }
                    if (color4 == backgroundColor)
                    {
                        microparticlePixel.PixelBorderEdges++;
                    }
                    if (microparticlePixel.PixelBorderEdges > 0)
                    {
                        microparticle.Border.Add(microparticlePixel);
                        microparticle.BorderEdges += microparticlePixel.PixelBorderEdges;
                    }
                }
                microparticle.D = Math.Log(microparticle.BorderEdges) / Math.Log(1 / microparticle.R);
                microparticle.L = Math.Pow(microparticle.R, 1 - microparticle.D) * Math.PI / 4;
                var s1 = Math.Pow(microparticle.R, 2) * (microparticle.CountOfPixels - microparticle.Border.Count);
                var s2 = Math.Pow(microparticle.R, 2) * microparticle.Border.Count * Math.PI / 4;
                microparticle.F = s1 + s2;
                microparticle.Coefficient = 2 *  Math.Sqrt(Math.PI * microparticle.F) / microparticle.L;
                microparticle.ClassOfMicroparticle = GetClassForMicroparticle(microparticle.Coefficient);
                if (microparticle.ClassOfMicroparticle != null)
                {
                    microparticle.ClassOfMicroparticle.Count++;
                }
            }

            Application.Current.Dispatcher.Invoke(delegate {
                Labels = new string[ClassesOfMicroparticles.Count];
                var columnSeries = new ColumnSeries
                {
                    Title = "Классификация микрочастиц",
                    Values = new ChartValues<int>()
                };
                var c = 0;
                foreach (var classeOfMicroparticles in ClassesOfMicroparticles)
                {
                    columnSeries.Values.Add(classeOfMicroparticles.Count);
                    Labels[c] = classeOfMicroparticles.Number.ToString();
                    c++;
                }
                SeriesCollection.Add(columnSeries);
            });

            ProcessedImage = newImage;
            IsDetailsVisibility = true;
            timer.Stop();
            StatusMessage = "Готово.";
            MessageBox.Show("Обработка снимка завершена. Время выполнения: " + MyTimer, "Внимание");
            IsStatusBarVisible = false;
        }

        private void ChangeTimer(object sender, ElapsedEventArgs e)
        {
            var tick = DateTime.Now.Ticks - StartDateTime.Ticks;
            var stopWatch = new DateTime();
            stopWatch = stopWatch.AddTicks(tick);
            MyTimer = $"{stopWatch:HH:mm:ss:ff}";
        }

        public Pixel FindNextPixel(Bitmap image, Color backgroundColor, Phase phase = null)
        {
            var tempPixels = new List<Pixel>();
            tempPixels.AddRange(TemPixelsMassive);
            foreach (var foundedPixel in TemPixelsMassive)
            {
                for (var i = foundedPixel.X1 - 1; i < foundedPixel.X1 + 2; i++)
                {
                    var startY = foundedPixel.X2 - 1;
                    if (i == foundedPixel.X1 - 1 || i == foundedPixel.X1 + 1)
                    {
                        startY = foundedPixel.X2;
                    }
                    for (var j = startY; j < foundedPixel.X2 + 2; j = j + 2)
                    {
                        if (i == foundedPixel.X1 && j == foundedPixel.X2)
                        {
                            continue;
                        }
                        try
                        {
                            var pixel = image.GetPixel(i, j);
                            var color = Color.FromArgb(pixel.R, pixel.G, pixel.B);
                            var nextPixel = new Pixel
                            {
                                X1 = i,
                                X2 = j,
                                Color = color
                            };
                            if (BlockPixels[i, j] || color == backgroundColor) continue;
                            if (Phases.Count > 0)
                            {
                                var foundedPhases = GetPhasesForPixel(color);
                                var isFounded = false;
                                foreach (var foundedPhase in foundedPhases)
                                {
                                    if (foundedPhase == phase)
                                    {
                                        isFounded = true;
                                    }
                                }
                                if (!isFounded)
                                {
                                    continue;
                                }
                            }
                            TemPixelsMassive = tempPixels;
                            return nextPixel;
                        }
                        catch (Exception e)
                        {
                            // ignored
                        }
                    }
                }
                tempPixels.Remove(foundedPixel);
            }
            TemPixelsMassive = tempPixels;
            return null;
        }

        public List<Phase> GetPhasesForPixel(Color color)
        {
            var foundedPhases = new List<Phase>();
            foreach (var phase in Phases)
            {
                foreach (var phaseColor in phase.Colors)
                {
                    var actualColor = phaseColor;
                    if (IsBlackAndWhite)
                    {
                        var R = (float)((phaseColor.ToArgb() & 0x00FF0000) >> 16);
                        var G = (float)((phaseColor.ToArgb() & 0x0000FF00) >> 8);
                        var B = (float)(phaseColor.ToArgb() & 0x000000FF);
                        R = G = B = (R + G + B) / 3.0f;
                        var newPixel = 0xFF000000 | ((UInt32)R << 16) | ((UInt32)G << 8) | ((UInt32)B);
                        actualColor = Color.FromArgb((int)newPixel);
                    }
                    var red = Math.Abs(actualColor.R - color.R);
                    var green = Math.Abs(actualColor.G - color.G);
                    var blue = Math.Abs(actualColor.B - color.B);
                    if (red < ColorSensitivity && green < ColorSensitivity && blue < ColorSensitivity)
                    {
                        foundedPhases.Add(phase);
                        break;
                    }
                }
            }
            return foundedPhases.Count > 0 ? foundedPhases : null;
        }

        public Class GetClassForMicroparticle(double coef)
        {
            foreach (var classesOfMicroparticle in ClassesOfMicroparticles)
            {
                if (coef * 10 > classesOfMicroparticle.Min && coef * 10 <= classesOfMicroparticle.Max)
                {
                    return classesOfMicroparticle;
                }
            }
            return null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
