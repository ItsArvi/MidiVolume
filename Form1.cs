using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using NAudio.CoreAudioApi;

namespace MidiVolume
{
    public partial class Form1 : Form
    {
        private InputDevice[] deviceList;
        private InputDevice? midiDevice;
        private int currentKnobId = 0;
        private bool isLinkingKnobMode = false;
        private bool isLinked = false;

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
                if (isLinkingKnobMode)
                {
                    currentKnobId = ccEvent.ControlNumber;
                    isLinkingKnobMode = false;
                    lblKnobNum.Text = $"Knob num: {currentKnobId}; Value: {ccEvent.ControlValue}";
                    isLinked = true;
                }
                if (isLinked && currentKnobId == ccEvent.ControlNumber)
                {
                    float volume = ccEvent.ControlValue / 127f;

                    this.Invoke(new Action(() =>
                    {
                        lblKnobNum.Text = $"Knob num: {currentKnobId}; Value: {ccEvent.ControlValue}; volume: {Math.Round(volume * 100f, 0)}";
                    }));

                    var enumerator = new MMDeviceEnumerator();
                    var device = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
                    device.AudioEndpointVolume.MasterVolumeLevelScalar = volume;
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopMidiListening();
            base.OnFormClosing(e);
        }

        private void StopMidiListening()
        {
            if (midiDevice != null)
            {
                midiDevice.StopEventsListening();
                midiDevice.Dispose();
            }
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
                bLinkKnob.Enabled = true;
                midiDevice = InputDevice.GetByIndex(cbMidiDevices.SelectedIndex);
            }
            else
            {
                cbMidiDevices.SelectedIndex = -1;
                cbMidiDevices.Text = string.Empty;
                cbMidiDevices.Items.Clear();
                MessageBox.Show("no midi devices found");
                bLinkKnob.Enabled = false;
                isLinked = false;
                lblKnobNum.Text = string.Empty;
            }
        }

        private void bRefresh_Click(object sender, EventArgs e)
        {
            RefreshMidiDevices();
        }

        private void bLinkKnob_Click(object sender, EventArgs e)
        {
            isLinkingKnobMode = true;
            lblKnobNum.Text = "Waiting for a value...";
        }

        private void cbMidiDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            midiDevice?.EventReceived -= OnMidiEventReceived;
            StopMidiListening();
            midiDevice = InputDevice.GetByIndex(cbMidiDevices.SelectedIndex);
            midiDevice.EventReceived += OnMidiEventReceived;
            midiDevice.StartEventsListening();
        }
    }
}
