using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PZ3_NetworkService.Model;

namespace PZ3_NetworkService.ViewModel
{
    public class NetworkViewViewModel : BindableBase
    {
        public static ObservableCollection<Pumpa> Pumpe { get; set; }

        bool dragDropActive = false;
        private Pumpa selectedPumpa = null;
        private Pumpa dragDropPumpa = null;

        private int minValue;
        private int maxValue;
        public static ObservableCollection<Pumpa> DroppedPumps { get; set; } 
        public static ObservableCollection<Pumpa> pomocna { get; set; }

        public MyICommand MouseLeftButtonUpCommand { get; set; }
        public MyICommand<Canvas> DropCommand { get; set; }
        public MyICommand<ListView> SelectionChangedCommand { get; set; }
        public MyICommand<Canvas> FreeCanv1Command { get; set; }

        public NetworkViewViewModel()
        {
            minValue = 670;
            maxValue = 735;
            Pumpe = MainViewModel.Pumpe;
            selectedPumpa = new Pumpa();
            dragDropPumpa = new Pumpa();
            DroppedPumps = new ObservableCollection<Pumpa>();

            pomocna = Pumpe;

            DropCommand = new MyICommand<Canvas>(OnDrop);
            SelectionChangedCommand = new MyICommand<ListView>(OnSelectionChanged);
            MouseLeftButtonUpCommand = new MyICommand(OnMouseLeftButtonUp);
            FreeCanv1Command = new MyICommand<Canvas>(OnFreeCanv1);
        }

 
        public Pumpa SelectedPumpa
        {

            get { return selectedPumpa; }

            set
            {
                if (selectedPumpa != value)
                {
                    selectedPumpa = value;
                    OnPropertyChanged("SelectedPumpa");
                }

            }
        }

        public Pumpa DragDropPumpa
        {

            get { return dragDropPumpa; }

            set
            {
                if (dragDropPumpa != value)
                {
                    dragDropPumpa = value;
                    OnPropertyChanged("DragDropPumpa");
                }

            }

        }


        public void OnDrop(Canvas canv)
        {
            if (DragDropPumpa != null)
            {

                if ((canv).Resources["taken"] == null) 
                {
                    BitmapImage img = new BitmapImage();
                    img.BeginInit();
                    img.UriSource = new Uri(DragDropPumpa.Type.ImgSrc);
                    img.EndInit();

                    (canv).Background = new ImageBrush(img);

                    ((TextBlock)(canv).Children[1]).Text = " ID[" + DragDropPumpa.Id.ToString() + "]  Name[" + DragDropPumpa.Name + "]"; 
                    
                    (canv).Resources.Add("taken", true);


                    for (int i = 0; i < pomocna.Count; i++)
                    {
                        if (pomocna[i].Id == DragDropPumpa.Id)
                        {

                             DroppedPumps.Add(pomocna[i]);
                              pomocna.RemoveAt(i);
                        }
                    }

                    OnPropertyChanged("pomocna");
                }
                DragDropPumpa = null;
                dragDropActive = false;

            }
        }


        private void OnSelectionChanged(ListView lvPumps)
        {

            if (!dragDropActive)
            {
                DragDropPumpa = new Pumpa(SelectedPumpa.Id, SelectedPumpa.Name, SelectedPumpa.Type, SelectedPumpa.Value);

                dragDropActive = true;
                DragDrop.DoDragDrop(lvPumps, DragDropPumpa, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }

        private void OnMouseLeftButtonUp()
        {
            DragDropPumpa = null;
            SelectedPumpa = null;
            dragDropActive = false;
        }


        private void OnFreeCanv1(Canvas canv)
        {
            if (canv.Resources["taken"] != null)//kz
            {

                string[] lineParts = ((TextBlock)(canv).Children[1]).Text.Split('[', ']');
                int id = Int32.Parse(lineParts[1]);

                foreach (Pumpa p in DroppedPumps.ToList())
                {
                    if (p.Id == id)
                    {
                          pomocna.Add(p);
                          DroppedPumps.Remove(p);
                    }
                }


                canv.Background = Brushes.Gray;
                ((TextBlock)(canv).Children[1]).Text = "Available";
                (canv).Resources.Remove("taken");
            }
        }
    }
}
