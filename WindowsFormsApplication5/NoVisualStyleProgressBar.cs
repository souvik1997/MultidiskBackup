using System.Windows.Forms;

namespace MultidiskBackup
{
    public partial class NoVisualStyleProgressBar
    {
        public NoVisualStyleProgressBar()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Opaque, true);
            InitializeComponent();
        }
    }
}
