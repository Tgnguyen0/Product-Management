using System.Globalization;
using QuanLySanPham.Object;
using QuanLySanPham.AppFunction;
using System.Text.RegularExpressions;
using Accessibility;
using System.Data;
using QuanLySanPham.dao;

namespace QuanLySanPham.gui;

public partial class MainPage : Form
{
    // GUI
    private FlowLayoutPanel container;
    private TableLayoutPanel formPanel;
    private TableLayoutPanel functionPanel;
    private Panel tablePanel;
    private DataTable dataTable;
    private DataGridView dataGridView;
    private Label idLabel;
    private TextBox idBox;
    private Label nameLabel;
    private TextBox nameBox;
    private Label shippedToLabel;
    private TextBox shippedToBox;
    private Label priceLabel;
    private TextBox priceBox;
    private Label dateLabel;
    private TextBox dateBox;
    private Label categoryLabel;
    private ComboBox categoryBox;
    private Button addBtn;
    private Button removeBtn;
    private Button findBtn;
    private TextBox findBox;
    private Button saveBtn;
    private Boolean[] isRowEmpty;

    // Object
    private QLSanPham listOfSP = new QLSanPham();
    private string ma;
    private string ten;
    private string noiNhap;
    private double giaNhap;
    private DateTime ngayNhap;
    private string loai;
    private int maLoaiSP;
    private NhomSP loaiSP;
    private string maTim;

    //DAO
    private ISanPhamDAO spd;

    public MainPage()
    {
        // Set the form's caption, which will appear in the title bar.
        this.Text = "Form";
        this.Size = new Size(1000, 600);
        this.MaximizeBox = false;
        this.BackColor = Color.GhostWhite;
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.StartPosition = FormStartPosition.CenterScreen;

        // Create a button control and set its properties.

        // Wire up an event handler to the button's "Click" event
        // (see the code in the btnHello_Click function below).

        // Add the button to the form's control collection,
        // so that it will appear on the form.
        //this.Controls.Add(btnHello);

        container = new FlowLayoutPanel();
        container.Size = new Size(1000, 550);

        MessageBox.Show($"Xem cách nhập thông tin chi tiết bằng cách để trỏ chuột lên mỗi tên", "Cách Nhập thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);

        createInputSection();
        createTable();
        createFunctionBar();

        this.Controls.Add(container);
    }

    //create a input section
    private void createInputSection() {
        formPanel = new TableLayoutPanel();
        formPanel.BackColor = Color.GhostWhite;
        formPanel.RowCount = 4;
        formPanel.Size = new Size(1000, 170);
        formPanel.Margin = new Padding(0, 0, 0, 0);
        formPanel.Dock = DockStyle.Top;

        // TopLeft: Căn văn bản về phía trên bên trái.
        // TopCenter: Căn văn bản về phía trên giữa.
        // TopRight: Căn văn bản về phía trên bên phải.
        // MiddleLeft: Căn văn bản về giữa bên trái.
        // MiddleCenter: Căn văn bản về giữa trung tâm.
        // MiddleRight: Căn văn bản về giữa bên phải.
        // BottomLeft: Căn văn bản về phía dưới bên trái.
        // BottomCenter: Căn văn bản về phía dưới giữa.
        // BottomRight: Căn văn bản về phía dưới bên phải.

        Label introduction = new Label();
        introduction.Size = new Size(1000, 50);
        introduction.BackColor = Color.FromArgb(94, 94, 94);
        introduction.ForeColor = Color.FromArgb(212, 141, 0);
        introduction.TextAlign = ContentAlignment.MiddleCenter;
        introduction.Text = "Quản Lý Sản Phẩm";
        introduction.Font = new Font("Consolas", 15, FontStyle.Regular);
        //introduction.AutoSize = true;
        introduction.Margin = new Padding(0, 0, 0, 0); // right, top, left, bottom
        formPanel.Controls.Add(introduction, 1, 0);
        formPanel.SetColumnSpan(introduction, 4);

        ToolTip idToolTip = new ToolTip();
        idToolTip.ToolTipTitle = "Thông tin chi tiết"; // Tiêu đề của tooltip
        idToolTip.ToolTipIcon = ToolTipIcon.Info; // Biểu tượng của tooltip
        idToolTip.IsBalloon = true; // Hiển thị tooltip dạng balloon
        idToolTip.AutoPopDelay = 4000; // Thời gian hiển thị tooltip (miliseconds)
        idToolTip.InitialDelay = 500; // Thời gian chờ trước khi hiển thị tooltip (miliseconds)
        idToolTip.ReshowDelay = 500; // Thời gian chờ giữa các lần hiển thị tooltip (miliseconds)

        idLabel = new Label();
        idLabel.Size = new Size(200, 25); // Chỉnh kích cỡ
        idLabel.Text = "Mã Sản Phẩm: "; // Chữ cho component
        idLabel.ForeColor = Color.Orange;
        // idLabel.BackColor = Color.FromArgb(255, 0, 1); // Màu nền
        idLabel.TextAlign = ContentAlignment.MiddleCenter; // Căn chữ
        idLabel.ForeColor = Color.FromArgb(212, 141, 0);
        idLabel.Font = new Font("Consolas", 10, FontStyle.Regular);
        //idLabel.AutoSize = true; // Tự động chỉnh kích thước cho phù hợp kích thước chữ
        idLabel.Margin = new Padding(50, 5, 0, 0); // right, top, left, bottom
        idToolTip.SetToolTip(idLabel, "Mã không được rỗng cũng như chỉ có chữ cái hoặc số"); // Thêm tooltip cho label

        idBox = new TextBox();
        idBox.Size = new Size(200, 25);
        idBox.BorderStyle = BorderStyle.FixedSingle;
        idBox.Margin = new Padding(10, 5, 50, 0);
        idBox.Tag = "id TextBox";
        idBox.Leave += new EventHandler(textBox_Leave);

        formPanel.Controls.Add(idLabel, 1, 1);
        formPanel.Controls.Add(idBox, 2, 1);

        ToolTip nameToolTip = new ToolTip();
        nameToolTip.ToolTipTitle = "Thông tin chi tiết"; // Tiêu đề của tooltip
        nameToolTip.ToolTipIcon = ToolTipIcon.Info; // Biểu tượng của tooltip
        nameToolTip.IsBalloon = true; // Hiển thị tooltip dạng balloon
        nameToolTip.AutoPopDelay = 4000; // Thời gian hiển thị tooltip (miliseconds)
        nameToolTip.InitialDelay = 500; // Thời gian chờ trước khi hiển thị tooltip (miliseconds)
        nameToolTip.ReshowDelay = 500; // Thời gian chờ giữa các lần hiển thị tooltip (miliseconds)

        nameLabel = new Label();
        nameLabel.Size = new Size(200, 25);
        nameLabel.Text = "Tên Sản Phẩm: ";
        // nameLabel.BackColor = Color.FromArgb(255, 0, 1);
        nameLabel.ForeColor = Color.FromArgb(212, 141, 0);
        nameLabel.Font = new Font("Consolas", 10, FontStyle.Regular);
        nameLabel.TextAlign = ContentAlignment.MiddleCenter;
        nameLabel.AutoSize = false;
        nameLabel.Margin = new Padding(10, 5, 0, 0); // right, top, left, bottom
        nameToolTip.SetToolTip(nameLabel, "Tên không được rỗng hoặc chứa kí tự số với các kí tự đặc biệt.");

        nameBox = new TextBox();
        nameBox.Size = new Size(200, 25);
        nameBox.BorderStyle = BorderStyle.FixedSingle;
        nameBox.Margin = new Padding(10, 5, 50, 0);
        nameBox.Tag = "name TextBox";
        nameBox.Leave += new EventHandler(textBox_Leave);

        formPanel.Controls.Add(nameLabel, 3, 1);
        formPanel.Controls.Add(nameBox, 4, 1);

        ToolTip shippedToToolTip = new ToolTip();
        shippedToToolTip.ToolTipTitle = "Thông tin chi tiết"; // Tiêu đề của tooltip
        shippedToToolTip.ToolTipIcon = ToolTipIcon.Info; // Biểu tượng của tooltip
        shippedToToolTip.IsBalloon = true; // Hiển thị tooltip dạng balloon
        shippedToToolTip.AutoPopDelay = 4000; // Thời gian hiển thị tooltip (miliseconds)
        shippedToToolTip.InitialDelay = 500; // Thời gian chờ trước khi hiển thị tooltip (miliseconds)
        shippedToToolTip.ReshowDelay = 500; // Thời gian chờ giữa các lần hiển thị tooltip (miliseconds)

        shippedToLabel = new Label();
        shippedToLabel.Size = new Size(200, 25); // Chỉnh kích cỡ
        shippedToLabel.Text = "Nơi Nhập SP: "; // Chữ cho component
        // shippedToLabel.BackColor = Color.FromArgb(255, 0, 1); // Màu nền
        shippedToLabel.ForeColor = Color.FromArgb(212, 141, 0);
        shippedToLabel.Font = new Font("Consolas", 10, FontStyle.Regular);
        shippedToLabel.TextAlign = ContentAlignment.MiddleCenter; // Căn chữ
        shippedToLabel.AutoSize = false; // Tự động chỉnh kích thước cho phù hợp kích thước chữ
        shippedToLabel.Margin = new Padding(50, 5, 0, 0); // right, top, left, bottom
        shippedToToolTip.SetToolTip(shippedToLabel, "Nơi nhậo không được rỗng");

        shippedToBox = new TextBox();
        shippedToBox.Size = new Size(200, 25);
        shippedToBox.BorderStyle = BorderStyle.FixedSingle;
        shippedToBox.Margin = new Padding(10, 5, 50, 0);
        shippedToBox.Tag = "shippedTo TextBox";
        //shippedToBox.Leave += new EventHandler(textBox_Leave);

        formPanel.Controls.Add(shippedToLabel, 1, 2);
        formPanel.Controls.Add(shippedToBox, 2, 2);

        ToolTip priceToolTip = new ToolTip();
        priceToolTip.ToolTipTitle = "Thông tin chi tiết"; // Tiêu đề của tooltip
        priceToolTip.ToolTipIcon = ToolTipIcon.Info; // Biểu tượng của tooltip
        priceToolTip.IsBalloon = true; // Hiển thị tooltip dạng balloon
        priceToolTip.AutoPopDelay = 4000; // Thời gian hiển thị tooltip (miliseconds)
        priceToolTip.InitialDelay = 500; // Thời gian chờ trước khi hiển thị tooltip (miliseconds)
        priceToolTip.ReshowDelay = 500; // Thời gian chờ giữa các lần hiển thị tooltip (miliseconds)

        priceLabel = new Label();
        priceLabel.Size = new Size(200, 25);
        priceLabel.Text = "Giá Sản Phẩm: ";
        // priceLabel.BackColor = Color.FromArgb(255, 0, 1);
        priceLabel.ForeColor = Color.FromArgb(212, 141, 0);
        priceLabel.Font = new Font("Consolas", 10, FontStyle.Regular);
        priceLabel.TextAlign = ContentAlignment.MiddleCenter;
        priceLabel.AutoSize = false;
        priceLabel.Margin = new Padding(10, 5, 0, 0); // right, top, left, bottom
        priceToolTip.SetToolTip(priceLabel, "Giá cả lớn hơn 0 và không được rỗng");

        priceBox = new TextBox();
        priceBox.Size = new Size(200, 25);
        priceBox.BorderStyle = BorderStyle.FixedSingle;
        priceBox.Margin = new Padding(10, 5, 50, 0);
        priceBox.Tag = "price TextBox";
        //priceBox.Leave += new EventHandler(textBox_Leave);

        formPanel.Controls.Add(priceLabel, 3, 2);
        formPanel.Controls.Add(priceBox, 4, 2);

        ToolTip dateToolTip = new ToolTip();
        dateToolTip.ToolTipTitle = "Thông tin chi tiết"; // Tiêu đề của tooltip
        dateToolTip.ToolTipIcon = ToolTipIcon.Info; // Biểu tượng của tooltip
        dateToolTip.IsBalloon = true; // Hiển thị tooltip dạng balloon
        dateToolTip.AutoPopDelay = 4000; // Thời gian hiển thị tooltip (miliseconds)
        dateToolTip.InitialDelay = 500; // Thời gian chờ trước khi hiển thị tooltip (miliseconds)
        dateToolTip.ReshowDelay = 500; // Thời gian chờ giữa các lần hiển thị tooltip (miliseconds)

        dateLabel = new Label();
        dateLabel.Size = new Size(200, 25); // Chỉnh kích cỡ
        dateLabel.Text = "Ngày Nhập SP: "; // Chữ cho component
        dateLabel.ForeColor = Color.FromArgb(212, 141, 0);
        // dateLabel.BackColor = Color.FromArgb(255, 0, 1); // Màu nền
        dateLabel.Font = new Font("Consolas", 10, FontStyle.Regular);
        dateLabel.TextAlign = ContentAlignment.MiddleCenter; // Căn chữ
        dateLabel.AutoSize = false; // Tự động chỉnh kích thước cho phù hợp kích thước chữ
        dateLabel.Margin = new Padding(50, 5, 0, 0); // right, top, left, bottom
        dateToolTip.SetToolTip(dateLabel, "Ngày Nhập phải trước hay cùng với ngày hiện tại và không được rỗng");

        dateBox = new TextBox();
        dateBox.Size = new Size(200, 25);
        dateBox.BorderStyle = BorderStyle.FixedSingle;
        dateBox.Margin = new Padding(10, 5, 50, 0);
        dateBox.Tag = "date TextBox";
        //dateBox.Leave += new EventHandler(textBox_Leave);

        formPanel.Controls.Add(dateLabel, 1, 3);
        formPanel.Controls.Add(dateBox, 2, 3);

        categoryLabel = new Label();
        categoryLabel.Size = new Size(200, 25);
        categoryLabel.Text = "Loại Sản Phẩm: ";
        categoryLabel.ForeColor = Color.FromArgb(212, 141, 0);
        // categoryLabel.BackColor = Color.FromArgb(255, 0, 1);
        categoryLabel.Font = new Font("Consolas", 10, FontStyle.Regular);
        categoryLabel.TextAlign = ContentAlignment.MiddleCenter;
        categoryLabel.AutoSize = false;
        categoryLabel.Margin = new Padding(10, 5, 0, 0); // right, top, left, bottom

        categoryBox = new ComboBox();
        categoryBox.Size = new Size(200, 25);
        //categoryBox.BorderStyle = BorderStyle.FixedSingle;
        categoryBox.Margin = new Padding(10, 5, 50, 0);
        categoryBox.Tag = "category TextBox";
        categoryBox.Font = new Font("Consolas", 10, FontStyle.Regular);

        categoryBox.Items.Add("Nhóm giá thấp");
        categoryBox.Items.Add("Nhóm giá trung bình");
        categoryBox.Items.Add("Nhóm giá cao");
        categoryBox.SelectedIndex = 0;

        //categoryBox.SelectedIndexChanged += new EventHandler(categoryBox_SelectedIndexChanged);

        formPanel.Controls.Add(categoryLabel, 3, 3);
        formPanel.Controls.Add(categoryBox, 4, 3);

        container.Controls.Add(formPanel);
    }

    // Create a table section
    private void createTable()
    {
        tablePanel = new Panel();
        tablePanel.Size = new Size(1000, 290);
        tablePanel.Margin = new Padding(0);

        dataTable = new DataTable();
        dataTable.Columns.Add("Mã Sản Phẩm", typeof(string));
        dataTable.Columns.Add("Tên Sản Phẩm", typeof(string));
        dataTable.Columns.Add("Nơi Nhập SP", typeof(string));
        dataTable.Columns.Add("Giá Sản Phẩm", typeof(decimal));
        dataTable.Columns.Add("Ngày Nhập SP", typeof(DateTime));
        dataTable.Columns.Add("Loại Sản Phẩm", typeof(string));

        spd = new SanPhamDAO();
        //listOfSP.themNhieuSanPham(spd.GetAllProducts());

        // Add sample data to DataTable
        for (int i = 0; i < listOfSP.soLuong(); i++)
        {
            SanPham sp = listOfSP.timSanPhamTheoTT(i);
            dataTable.Rows.Add(sp.Ma, sp.Ten, sp.NoiNhap, sp.GiaNhap, sp.NgayNhap, sp.Loai);
        }

        dataGridView = new DataGridView();
        dataGridView.DataSource = dataTable;
        dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView.Height = 290;  // Adjust the height as needed
        dataGridView.Width = 985;  // Adjust the width as needed
        dataGridView.Font = new Font("Consolas", 10, FontStyle.Regular);
        dataGridView.AllowUserToAddRows = false;
        dataGridView.AllowUserToDeleteRows = false;
        dataGridView.ReadOnly = true;
        dataGridView.BorderStyle = BorderStyle.None;
        dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.None;
        dataGridView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        dataGridView.EnableHeadersVisualStyles = false;
        dataGridView.Margin = new Padding(0);
        dataGridView.ScrollBars = ScrollBars.Vertical;
        dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

        // Align cell contents
        foreach (DataGridViewColumn column in dataGridView.Columns)
        {
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        // Custom painting to draw bottom border for column headers
        dataGridView.Paint += (sender, e) =>
        {
            using (Pen pen = new Pen(Color.Black, 1))
            {
                // Draw the bottom border of the column headers
                Rectangle headerRect = dataGridView.DisplayRectangle;
                headerRect.Height = dataGridView.ColumnHeadersHeight;
                e.Graphics.DrawLine(pen, headerRect.Left, headerRect.Bottom - 1, headerRect.Right, headerRect.Bottom - 1);
            }
        };

        // Custom row painting to draw bottom border for each row
        dataGridView.RowPostPaint += (sender, e) =>
        {
            using (Pen pen = new Pen(Color.Black, 1))
            {
                // Calculate the rectangle of the current row
                Rectangle rowRect = e.RowBounds;
                rowRect.Height--;

                // Draw the bottom border of the current row
                e.Graphics.DrawLine(pen, rowRect.Left, rowRect.Bottom - 1, rowRect.Right, rowRect.Bottom - 1);
            }
        };

        tablePanel.Controls.Add(dataGridView);

        // Add Panel containing DataGridView to the form or container
        container.Controls.Add(tablePanel);
    }


    public void createFunctionBar() {
        functionPanel = new TableLayoutPanel();
        functionPanel.ColumnCount = 6;
        functionPanel.RowCount = 1;
        functionPanel.BackColor = Color.GhostWhite;
        functionPanel.Size = new Size(1000, 50);
        functionPanel.Margin = new Padding(0, 0, 0, 0);

        addBtn = new Button();
        addBtn.Text = "Thêm";
        addBtn.AutoSize = true;
        addBtn.Size = new Size(100, 30);
        addBtn.BackColor = Color.FromArgb(212, 141, 0);
        addBtn.ForeColor = Color.FromArgb(94, 94, 94);
        addBtn.Margin = new Padding(180, 3, 0, 0);
        addBtn.Font = new Font("Consolas", 10, FontStyle.Regular);
        
        // Thay đổi border style
        addBtn.FlatAppearance.BorderSize = 1;

        // Thay đổi màu border
        addBtn.FlatAppearance.BorderColor = Color.FromArgb(212, 141, 0);

        // Thay đổi border style khi button được nhấn xuống
        //addBtn.FlatAppearance.MouseDownBackColor = Color.Green;

        // Thay đổi border style khi button có con trỏ di chuyển qua
        //addBtn.FlatAppearance.MouseOverBackColor = Color.Yellow;

        // Áp dụng màu và style vào button
        addBtn.FlatStyle = FlatStyle.Flat;
        addBtn.Click += new EventHandler(addBtn_Click);

        functionPanel.Controls.Add(addBtn, 0, 0);

        findBtn = new Button();
        findBtn.Text = "Tìm";
        findBtn.AutoSize = true;
        findBtn.Size = new Size(100, 30);
        findBtn.BackColor = Color.FromArgb(212, 141, 0);
        findBtn.ForeColor = Color.FromArgb(94, 94, 94);
        findBtn.Font = new Font("Consolas", 10, FontStyle.Regular);
        
        // Thay đổi border style
        findBtn.FlatAppearance.BorderSize = 1;

        // Thay đổi màu border
        findBtn.FlatAppearance.BorderColor = Color.FromArgb(212, 141, 0);

        // Thay đổi border style khi button được nhấn xuống
        //addBtn.FlatAppearance.MouseDownBackColor = Color.Green;

        // Thay đổi border style khi button có con trỏ di chuyển qua
        //addBtn.FlatAppearance.MouseOverBackColor = Color.Yellow;

        // Áp dụng màu và style vào button
        findBtn.FlatStyle = FlatStyle.Flat;
        findBtn.Click += new EventHandler(findBtn_Click);

        functionPanel.Controls.Add(findBtn, 1, 0);

        findBox = new TextBox();
        findBox.Size = new Size(addBtn.Width * 2, 30);
        findBox.BorderStyle = BorderStyle.FixedSingle;
        findBox.Margin = new Padding(0, 6, 0, 5);
        findBox.Tag = "find TextBox";
        findBox.Leave += new EventHandler(textBox_Leave);

        functionPanel.Controls.Add(findBox, 2, 0);

        removeBtn = new Button();
        removeBtn.Text = "Xoá";
        removeBtn.AutoSize = true;
        removeBtn.Size = new Size(100, 30);
        removeBtn.BackColor = Color.FromArgb(212, 141, 0);
        removeBtn.ForeColor = Color.FromArgb(94, 94, 94);
        removeBtn.Font = new Font("Consolas", 10, FontStyle.Regular);
        
        // Thay đổi border style
        removeBtn.FlatAppearance.BorderSize = 1;

        // Thay đổi màu border
        removeBtn.FlatAppearance.BorderColor = Color.FromArgb(212, 141, 0);

        // Thay đổi border style khi button được nhấn xuống
        //addBtn.FlatAppearance.MouseDownBackColor = Color.Green;

        // Thay đổi border style khi button có con trỏ di chuyển qua
        //addBtn.FlatAppearance.MouseOverBackColor = Color.Yellow;

        // Áp dụng màu và style vào button
        removeBtn.FlatStyle = FlatStyle.Flat;
        removeBtn.Click += new EventHandler(removeBtn_Click);

        functionPanel.Controls.Add(removeBtn, 3, 0);

        saveBtn = new Button();
        saveBtn.Text = "Lưu";
        saveBtn.AutoSize = true;
        saveBtn.Size = new Size(100, 30);
        saveBtn.BackColor = Color.FromArgb(212, 141, 0);
        saveBtn.ForeColor = Color.FromArgb(94, 94, 94);
        saveBtn.Font = new Font("Consolas", 10, FontStyle.Regular);
        
        // Thay đổi border style
        saveBtn.FlatAppearance.BorderSize = 1;

        // Thay đổi màu border
        saveBtn.FlatAppearance.BorderColor = Color.FromArgb(212, 141, 0);

        // Thay đổi border style khi button được nhấn xuống
        //addBtn.FlatAppearance.MouseDownBackColor = Color.Green;

        // Thay đổi border style khi button có con trỏ di chuyển qua
        //addBtn.FlatAppearance.MouseOverBackColor = Color.Yellow;

        // Áp dụng màu và style vào button
        saveBtn.FlatStyle = FlatStyle.Flat;
        saveBtn.Click += new EventHandler(saveBtn_Click);

        functionPanel.Controls.Add(saveBtn, 4, 0);

        container.Controls.Add(functionPanel);
    }

    private void textBox_Leave(object sender, EventArgs e)
    {
        TextBox textBox = sender as TextBox;
        string tag = textBox.Tag.ToString();
        string[] pattern = {
            "([^\\w\\d])+" , 
            "([^a-zA-ZÀ-ỹĂăÂâĐđÊêÔôƠơƯưƵƶáàảãạăắằẳẵặâấầẩẫậđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵ\\s])+"
        }; // Các pattern

        switch(tag) {
            case "id TextBox":
                ma = textBox.Text;

                // Kiểm tra mã hợp lệ
                if (Regex.IsMatch(ma, pattern[0]) && ma != null) {
                    MessageBox.Show($"Mã không hợp lệ: {ma}", "Lỗi Nhập Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox.Focus();
                    textBox.Text = "";
                }

                break;
            case "name TextBox":
                ten = textBox.Text;

                // Kiểm tra tên hợp lệ
                if (Regex.IsMatch(ten, pattern[1]) && ten != null) {
                    MessageBox.Show($"Tên không hợp lệ: {ten}", "Lỗi Nhập Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox.Focus();
                    textBox.Text = "";
                }

                break;
            case "shippedTo TextBox":
                noiNhap = textBox.Text;
                break;
            case "price TextBox":
                if (!double.TryParse(textBox.Text, out giaNhap) && giaNhap > 0) {
                    MessageBox.Show($"Giá không hợp lệ: {giaNhap}", "Lỗi Nhập Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox.Focus();
                    textBox.Text = "";
                }
                
                break;
            case "date TextBox":
                if (!DateTime.TryParseExact(textBox.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ngayNhap) && ngayNhap <= DateTime.Now) {
                    MessageBox.Show($"Ngày nhập không hợp lệ", "Lỗi Nhập Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox.Focus();
                }

                break;
            case "find TextBox":
                maTim = textBox.Text;

                // Kiểm tra mã hợp lệ
                if (Regex.IsMatch(maTim, pattern[0]) && maTim != null) {
                    MessageBox.Show($"Mã không hợp lệ: {maTim}", "Lỗi Nhập Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox.Focus();
                    textBox.Text = "";
                }

                break;
        }
    }

    private void categoryBox_SelectedIndexChanged(object sender, EventArgs e) 
    {
        loai = categoryBox.SelectedItem.ToString();
        MessageBox.Show($"Đã chọn: {loai}");
    }
     
    private void addBtn_Click(object sender, EventArgs e)
    {
        ma = idBox.Text;
        ten = nameBox.Text;
        noiNhap = shippedToBox.Text;
        double.TryParse(priceBox.Text, out double giaNhap);
        DateTime.TryParseExact(dateBox.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ngayNhap);
        loai = categoryBox.SelectedItem.ToString();

        SanPham sp = new SanPham(ma, ten, noiNhap, giaNhap, ngayNhap, new NhomSP(loai));
        
        if (sp != listOfSP.timSanPhamTheoMa(ma) && !ma.Equals(null) && !ten.Equals(null)) {
            listOfSP.themSanPham(sp);
            dataTable.Rows.Add($"{sp.Ma}", $"{sp.Ten}", $"{sp.NoiNhap}", $"{sp.GiaNhap}", $"{sp.NgayNhap}", $"{sp.Loai.TenNhom}");
        }
    }

    private void removeBtn_Click(object sender, EventArgs e) {
        if (dataGridView.SelectedRows.Count > 0) {
            foreach (DataGridViewRow row in dataGridView.SelectedRows) {
                if (!row.IsNewRow) {
                    dataGridView.Rows.Remove(row);
                }
            }
        } else {
            MessageBox.Show("Vui lòng chọn một hàng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); // Xuất hiện nếu không chọn
        }
    }

    private void findBtn_Click(object sender, EventArgs e) {
        maTim = findBox.Text;

        foreach (DataGridViewRow row in dataGridView.Rows)
        {
            // Kiếm từng hàng mà có mã tìm kiếm tương thích
            if (row.Cells["Mã Sản Phẩm"].Value != null && row.Cells["Mã Sản Phẩm"].Value.ToString().Equals(maTim))
            {
                row.Selected = true; // Highlight hàng đó
                dataGridView.FirstDisplayedScrollingRowIndex = row.Index; // Di chuyển đến hàng
                return;
            }
        }
    }

    private void saveBtn_Click(object sender, EventArgs e) {
        spd.SaveProduct(listOfSP.getListSP());
    }
}
