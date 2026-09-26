using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;

namespace MidiVolume
{
    public partial class Form1 : Form
    {
        private InputDevice[] deviceList;
        private InputDevice? midiDevice;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            deviceList = InputDevice.GetAll().ToArray();
            comboBox1.Items.Clear();
            if(deviceList.Length > 0)
            {
                foreach (var device in deviceList)
                {
                    comboBox1.Items.Add(device.Name);
                }
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("no midi devices found");
            }
        }

        private void OnMidiEventReceived(object? sender, MidiEventReceivedEventArgs e)
        {
            if (e.Event is ControlChangeEvent ccEvent)
            {
                int knobNum = ccEvent.ControlNumber;
                int knobVal = ccEvent.ControlValue;

                this.BeginInvoke(new Action(() =>
                {
                    this.Text = $"{knobNum} - {knobVal}";
                }));
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (midiDevice != null)
            {
                midiDevice.StopEventsListening();
                midiDevice.Dispose();
            }
            base.OnFormClosing(e);
        }
    }
}
