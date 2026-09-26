using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;

namespace MidiVolume
{
    public partial class Form1 : Form
    {
        private InputDevice? midiDevice;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var inputDevices = InputDevice.GetAll();

            midiDevice = inputDevices.ToArray()[0];
            midiDevice.EventReceived += OnMidiEventReceived;
            midiDevice.StartEventsListening();
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
