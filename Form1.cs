using MapPolygonWriter.Core;
using MapPolygonWriter.Core.MapServices;
using System;
using System.Windows.Forms;

namespace MapPolygonWriter
{
    public partial class Form1 : Form
    {
        string[] _availabelMapServices = new[]
        {
            "Open Street Map"
        };
        const int MAP_SERVICE_OSM = 0;

        public Form1()
        {
            InitializeComponent();

            domainUpDown1.Items.AddRange(_availabelMapServices);
            domainUpDown1.SelectedIndex = 0;

            RefreshStepHint();

            textBox2.Text = "test.txt";

            button1.Click += Button1_Click;
            trackBar1.ValueChanged += (s, e) => RefreshStepHint();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var address = textBox1.Text; 
            var path = textBox2.Text;

            if (string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(path))
            {
                MessageBox.Show("Не все поля заполнены!");
                return;
            }

            int step = (int)trackBar1.Value;

            var mapService = Str2MapService(domainUpDown1.SelectedItem.ToString());
            var worker = new MapPolygonWorker(mapService);

            worker.UnloadPolygon(address, path, step);
        }

        private void RefreshStepHint()
        {
            label5.Text = $"Текущий шаг: {trackBar1.Value}";
        }

        private IMapService Str2MapService(string str)
        {
            if (str == _availabelMapServices[MAP_SERVICE_OSM])
            {
                return new OsmMapService();
            }
            else
            {
                throw new Exception("");
            }
        }
    }
}
