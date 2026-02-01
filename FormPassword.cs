namespace DMARCReportAnalyzer;

/// <summary>
/// A form that allows the user to enter a password.
/// </summary>
public partial class FormPassword : Form
{
    /// <summary>
    /// The password entered by the user.
    /// </summary>
    private string Password;

    /// <summary>
    /// Constructor to create an instance of this form.
    /// </summary>
    public FormPassword()
    {
        InitializeComponent();
        this.Password = string.Empty;
    }

    /// <summary>
    /// Constructor to create an instance of this form.
    /// </summary>
    /// <param name="title">A title to be used on this form.</param>
    public FormPassword(string title) : this()
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            this.Text = "Password";
        }
        else
        {
            this.Text = title.Trim();
        }
    }

    /// <summary>
    /// Retrieves the password entered by the user.
    /// </summary>
    /// <returns>The password entered by the user.</returns>
    public string GetPassword()
    {
        return this.Password;
    }

    /// <summary>
    /// Event that is called when the user confirms the password.
    /// </summary>
    private void BtnOK_Click(object sender, EventArgs e)
    {
        if (CbNoPassword.CheckState == CheckState.Checked )
        {
            this.Password = string.Empty;
            txtPassword.Text = string.Empty;
            this.DialogResult = DialogResult.OK;
        }
        else
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                txtPassword.Focus();
                this.DialogResult = DialogResult.None;
            }
            else
            {
                this.Password = txtPassword.Text;
                this.DialogResult = DialogResult.OK;
            }
        }
    }

    /// <summary>
    /// Event that is called when the user does not confirm the password.
    /// </summary>
    private void BtnCancel_Click(object sender, EventArgs e)
    {
        this.Password = string.Empty;
        this.DialogResult = DialogResult.Cancel;
    }

    /// <summary>
    /// Event that is called when the user disables or enables the use of a password.
    /// </summary>
    private void CbNoPassword_CheckStateChanged(object sender, EventArgs e)
    {
        txtPassword.Enabled = (CbNoPassword.CheckState == CheckState.Unchecked);
    }
}