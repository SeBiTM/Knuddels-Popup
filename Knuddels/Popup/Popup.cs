using Knuddels.Popup.Components;
using Knuddels.Popup.Layout;
using Knuddels.Popup.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knuddels.Popup
{
    class Popup
    {
        private String title;
        private int width, height;
        private List<IComponent> components;
        private String opcode, parameter;
        private int[] background;

        public Popup(String title, int width, int height)
        {
            this.title = title;
            this.width = width;
            this.height = height;
            this.components = new List<IComponent>();
            this.background = new int[] { 0xBE, 0xBC, 0xFB };
        }

        public void AddComponent(IComponent com)
        {
            this.components.Add(com);
        }
        public void SetOpcode(String opcode, String parameter)
        {
            this.opcode = opcode;
            this.parameter = parameter;
        }

        public override string ToString()
        {
            PopupWriter buffer = new PopupWriter("k");
            buffer.WriteNull();
            buffer.WritePopupString(this.title);
            if (this.opcode != null)
            {
                buffer.Write('s');
                buffer.WritePopupString(this.opcode);
                buffer.WritePopupString(this.parameter);
            }
            if (this.width > 0 && this.height > 0)
            {
                buffer.Write('w');
                buffer.WriteShort(this.width);
                buffer.WriteShort(this.height);
            }

            buffer.WriteForeground(new int[] { 0x00, 0x00, 0x00 });
            buffer.WriteBackground(this.background);
            buffer.Write('C');
            buffer.WriteEnd();

            foreach (IComponent com in this.components)
                WriteComponent(com, buffer);
            buffer.WriteEnd();
            
            return buffer.ToString();
        }
        private static void WriteComponent(IComponent com, PopupWriter buffer)
        {
            if (com.Location != Location.NONE)
                buffer.Write((char)com.Location);

            buffer.Write((char)com.Type);

            switch (com.Type)
            {
                case ComponentType.LABEL:
                    buffer.WritePopupString(com.Text);
                    buffer.WriteFontStyle(((Label)com).Style, ((Label)com).Size);
                    break;
                case ComponentType.PANEL:
                    Panel panel = (Panel)com;
                    if (!string.IsNullOrEmpty(panel.ID))
                        buffer.WritePopupString(panel.ID);

                    if (panel.Background != null)
                        buffer.WriteBackground(panel.Background, 'b');
                    
                    if (panel.BackgroundImage != null)
                    {
                        buffer.Write('U');
                        buffer.WritePopupString(panel.BackgroundImage);
                        buffer.Write('U');
                        buffer.WriteShort(panel.Width);
                        buffer.WriteShort(panel.Height);
                    }
                    if (panel.Layout != null)
                    {
                        buffer.WriteLayout((char)panel.Layout.Type);
                        switch (panel.Layout.Type)
                        {
                            case LayoutType.GRID_LAYOUT:
                                GridLayout grid = (GridLayout)panel.Layout;
                                buffer.WriteSize(grid.Rows);
                                buffer.WriteSize(grid.Cols);
                                buffer.WriteSize(grid.HGap);
                                buffer.WriteSize(grid.VGap);
                                break;
                        }
                    }
                    var coms = panel.Components;
                    foreach (var c in coms)
                        WriteComponent(c, buffer);
                    break;
                case ComponentType.TEXT_PANEL:
                    TextPanel tp = (TextPanel)com;
                    buffer.WritePopupString(string.Format("°R>{{linkhovercolor}}<r°{0}°>{{linkhovercolorreset}}<°", com.Text));
                    buffer.WriteFrameSize(tp.Width, tp.Height);
                    buffer.WriteBackgroundImage(tp.BackgroundImage, tp.BackgroundPosition);
                    break;
                case ComponentType.BUTTON:
                    buffer.WritePopupString(com.Text);
                    Button button = (Button)com;
                    if (button.FontSize != 14 || button.FontWeight != 'p')
                        buffer.WriteFontStyle(button.FontWeight, button.FontSize);

                    if (button.Styled)
                    {
                        buffer.Write('c');
                        if (button.Colored)
                            buffer.Write('e');
                    }

                    if (button.Close)
                        buffer.Write('d');
                    if (button.Action)
                        buffer.Write('s');

                    // KP ?
                    buffer.Write('b');
                    buffer.Write('g');
                    buffer.Write('O');

                    if (button.Command != null)
                    {
                        buffer.Write('u');
                        buffer.WritePopupString(button.Command);
                    }
                    break;
                case ComponentType.TEXT_FIELD:
                    buffer.WritePopupString(com.Text);
                    buffer.WriteSize(((TextField)com).Width);
                    break;
                case ComponentType.TEXT_AREA:
                    buffer.WritePopupString(com.Text);
                    TextArea textarea = (TextArea)com;
                    buffer.WriteSize(textarea.Rows);
                    buffer.WriteSize(textarea.Cols);

                    switch (textarea.Scrollbars)
                    {
                        case 0:
                            buffer.Write('b');
                            break;
                        case 1:
                            buffer.Write('s');
                            break;
                        case 2:
                            buffer.Write('w');
                            break;
                    }
                    if (textarea.Editable)
                        buffer.Write('e');
                    break;
                case ComponentType.CHECKBOX:
                    if (com.Text != null)
                    {
                        buffer.Write('l');
                        buffer.WritePopupString(com.Text);
                    }

                    buffer.WriteFontStyle('p', 16);
                    Checkbox checkbox = (Checkbox)com;

                    if (checkbox.Disabled)
                        buffer.Write('d');

                    if (checkbox.Selected)
                    {
                        buffer.Write('s');
                        buffer.Write('t');
                    }

                    if (checkbox.Group != 0)
                    {
                        buffer.Write('r');
                        buffer.WriteSize(checkbox.Group);
                    }
                    break;
                case ComponentType.CHOICE:
                    Choice choice = (Choice)com;
                    if (choice.Selected == null)
                    {
                        buffer.Write('c');
                        buffer.Write(choice.SelectedIndex);
                    }
                    else
                    {
                        buffer.Write('C');
                        buffer.WritePopupString(choice.Selected);
                    }

                    if (choice.Fontsize > 0)
                        buffer.WriteFontStyle('p', choice.Fontsize);

                    if (choice.Disabled)
                        buffer.Write('d');

                    buffer.WriteForeground(com.Foreground);
                    buffer.WriteBackground(com.Background);

                    buffer.WriteEnd();
                    foreach (var item in choice.Items)
                    {
                        buffer.WritePopupString(item);
                    }
                    break;
            }
            if (com.Type != ComponentType.PANEL && com.Type != ComponentType.CHOICE)
            {
                if (com.Background != new int[] { 255, 255, 255 })
                    buffer.WriteBackground(com.Background);
                if (com.Foreground != new int[] { 0, 0, 0 })
                    buffer.WriteForeground(com.Foreground);
            }

            buffer.WriteEnd();
        }

        public static String Create(String title, String subtitle, String message, int width, int height, bool btn)
        {
            if (!btn)
                return Create(title, subtitle, message, width, height);
            return Create(title, subtitle, message, width, height, new List<IComponent>()
            {
                new Button("      OK     ")
            });
        }
        public static String Create(String title, String subtitle, String message, int width, int height, List<IComponent> controls = null) { 
            Popup popup = new Popup(title, width, height);

            Panel contentPanel = new Panel(new BorderLayout(), Location.CENTER);
            contentPanel.AddComponent(new TextPanel(
                            message, width - 10, height - 10,
                            Location.CENTER
                    )); //KCode

            if (controls != null)
            {
                Panel buttonPanel = new Panel(new FlowLayout(), Location.SOUTH);
                foreach (var com in controls)
                    buttonPanel.AddComponent(com);
                AddOldStyle(popup, subtitle, contentPanel, buttonPanel);
            }
            else
            {
                AddOldStyle(popup, subtitle, contentPanel, null);
            }

            return popup.ToString();
        }
        public static void AddOldStyle(Popup popup, String subtitle, Panel contentPanel, Panel buttonPanel, bool padding = true)
        {
            if (padding)
            {
                Label pAddingRight = new Label("         ", Location.EAST, 5); //PAdding Right
                popup.AddComponent(pAddingRight);

                Label pAddingLeft = new Label("         ", Location.WEST, 5); //PAdding Left
                popup.AddComponent(pAddingLeft);
            }
            Panel topPanel = new Panel(Location.CENTER);
            popup.AddComponent(topPanel);

            Panel headerPanel = new Panel(Location.NORTH);
            topPanel.AddComponent(headerPanel);

            headerPanel.AddComponent(new Label(" ", Location.NORTH, 5)); //PAdding Top
            if (subtitle != null)
                headerPanel.AddComponent(new Label(subtitle, Location.CENTER, 18, 'b', new int[] { 0xE5, 0xE5, 0xFF })); //Subtitle
            headerPanel.AddComponent(new Label(" ", Location.SOUTH, 5)); //PAdding Header <-> Content

            topPanel.AddComponent(contentPanel);
            if (buttonPanel != null)
                topPanel.AddComponent(buttonPanel);
        }

        public static String CreateNew(String title, String subtitle, String message, int width, int height)
        {
            Popup popup = new Popup(title, width, height);

            TextPanel kcPanel = new TextPanel(
                    message, 
                    width - 10, 
                    height - 10,
                    new int[] { 0, 0, 0 },
                    new int[] { 0xBE, 0xBC, 0xFB },
                    "pics/layout/bg_trend.png", 
                    -1,
                    Location.CENTER
                );

            AddNewStyle(popup, subtitle, kcPanel, new List<IComponent>()
            {
                new Button("     OK     ", Location.CENTER)
                {
                    Colored = true,
                    Styled   = true,
                    Close = true,
                    FontSize = 16,
                    FontWeight = 'b'
                }
            });

            return popup.ToString();
        }
        public static void AddNewStyle(Popup popup, String subtitle, TextPanel kcPanel, List<IComponent> controls)
        {
            popup.background = new int[] { 255, 255, 255 };

            var framePaddingPanel = new Panel(new BorderLayout(), Location.CENTER);
            framePaddingPanel.AddComponent(new Panel(null, Location.EAST, "pics/layout/bg_trend.png", 1, 10));
            framePaddingPanel.AddComponent(new Panel(null, Location.WEST, "pics/layout/bg_trend.png", 1, 10));
            framePaddingPanel.AddComponent(new Panel(null, Location.NORTH, "-", 10, 1));
            framePaddingPanel.AddComponent(new Panel(null, Location.SOUTH, "-", 10, 1)
            {
                Background  = new int[] { 235, 235, 255 }
            });

            popup.AddComponent(framePaddingPanel);

            var baseFramePanel = new Panel(new BorderLayout(), Location.CENTER);
            framePaddingPanel.AddComponent(baseFramePanel);

            #region Controls

            var southOuterGridPanel = new Panel(new GridLayout(1, 1, 1, 1), Location.SOUTH)
            {
                Background = new int[] { 235,235,255 }
            };
            baseFramePanel.AddComponent(southOuterGridPanel);

            var southFlowPanel = new Panel(new FlowLayout());
            southOuterGridPanel.AddComponent(southFlowPanel);

            var southGridPanel = new Panel(new GridLayout(0, 1, 1, 1));
            southFlowPanel.AddComponent(southGridPanel);

            foreach (var control in  controls)
            {
                var controlPanel = new Panel(new BorderLayout(), Location.NONE);
                southGridPanel.AddComponent(controlPanel);
                
                controlPanel.AddComponent(control);
            }

            #endregion

            var borderLayoutPanel = new Panel(new BorderLayout(), Location.NORTH);
            baseFramePanel.AddComponent(borderLayoutPanel);

            #region Header

            #region Header Top Border

            var northPanelOuter = new Panel(new BorderLayout(), Location.NORTH);
            borderLayoutPanel.AddComponent(northPanelOuter);

            var northPanel = new Panel(new BorderLayout(), Location.NORTH);
            northPanelOuter.AddComponent(northPanel);
            northPanel.AddComponent(new Panel(new BorderLayout(), Location.WEST, "pics/layout/boxS_tl.png", 16, 16));
            northPanel.AddComponent(new Panel(new BorderLayout(), Location.CENTER, "pics/layout/boxS_tc.png", 16, 16));
            northPanel.AddComponent(new Panel(new BorderLayout(), Location.EAST, "pics/layout/boxS_tr.png", 16, 16));

            #endregion

            #region Header Content Left Right

            borderLayoutPanel.AddComponent(new Panel(new BorderLayout(), Location.WEST, "pics/layout/boxS_cl.png", 16, 16));
            borderLayoutPanel.AddComponent(new Panel(new BorderLayout(), Location.EAST, "pics/layout/boxS_cr.png", 16, 16));

            #endregion

            #region Bottom Border

            var borderSouthPanel = new Panel(null, Location.SOUTH);
            borderLayoutPanel.AddComponent(borderSouthPanel);
            borderSouthPanel.AddComponent(new Panel(new BorderLayout(), Location.WEST, "pics/layout/boxS_bl.png", 16, 16));
            borderSouthPanel.AddComponent(new Panel(new BorderLayout(), Location.CENTER, "pics/layout/boxS_bc.png", 16, 16));
            borderSouthPanel.AddComponent(new Panel(new BorderLayout(), Location.EAST, "pics/layout/boxS_br.png", 16, 16));

            #endregion

            #region Header Center Label

            var borderCenterPanel = new Panel(new GridLayout(0,1,1,1), Location.CENTER);
            var borderCenterLabelPanel = new Panel(new BorderLayout(), Location.CENTER);
            borderCenterPanel.AddComponent(borderCenterLabelPanel);

            borderLayoutPanel.AddComponent(new Label(subtitle, Location.CENTER, 18, 'b')
            {
                Background = new int[] { 222,222,255 }
            });

            #endregion

            #endregion

            #region Content

            var contentLayoutPanel = new Panel(new BorderLayout(), Location.CENTER);
            baseFramePanel.AddComponent(contentLayoutPanel);

            var  contentCenterPanel = new Panel(new BorderLayout(), Location.CENTER, "-", 15, 1);
            contentLayoutPanel.AddComponent(contentCenterPanel);

            contentCenterPanel.AddComponent(new Panel(new BorderLayout(), Location.NORTH, "-", 4, 1));

            var contentPanel = new Panel(new BorderLayout(), Location.CENTER);
            contentCenterPanel.AddComponent(contentPanel);

            contentPanel.AddComponent(kcPanel);

            contentCenterPanel.AddComponent(new Panel(new BorderLayout(), Location.EAST, "pics/layout/bg_trend.png", 1, 16));
            contentCenterPanel.AddComponent(new Panel(new BorderLayout(), Location.WEST, "pics/layout/bg_trend.png", 1,16));

            #endregion
        }
    }
}