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
            RefreshMidiDevices();
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

        private void RefreshMidiDevices()
        {
            deviceList = InputDevice.GetAll().ToArray();
            cbMidiDevices.Items.Clear();
            if (deviceList.Length > 0)
            {
                foreach (var device in deviceList)
                {
                    cbMidiDevices.Items.Add(device.Name);
                }
                cbMidiDevices.SelectedIndex = 0;
            }
            else
            {
                cbMidiDevices.SelectedIndex = -1;
                cbMidiDevices.Text = string.Empty;
                cbMidiDevices.Items.Clear();
                MessageBox.Show("no midi devices found");
            }
        }

        private void bRefresh_Click(object sender, EventArgs e)
        {
            RefreshMidiDevices();
        }
    }
}
