namespace SolvencyII.Domain.Constants
{
    /// <summary>
    /// Constants used for creating the templates, used mainly within the user control generator.
    /// iOS settings currently not needed.
    /// </summary>
    public class ConstantsForDesigner
    {
        public int LABEL_COLUMN_WIDTH = 275;
        public int ROW_HEIGHT = 20; // 17 too small with DateTimePicker being 20
        public int CURRENCY_COLUMN_WIDTH = 107;
        public int CURRENCY_Width = 100;
        public int CURRENCY_Height = 13;

        public int USER_COMBO_Width = 200;
        public int USER_COMBO_COLUMN_WIDTH;

        
        public int LEVEL_TAB = 7;
        public int LABEL_Width = 108; // 106
        public int CODE_COLUMN_WIDTH = 50; //108; // 35
        
        public int LABEL_Height = 13;
        public int LABEL_Height_Division = 15;
        public int COMBO_Width = 250;
        public int COMBO_Height = 13;
        public int COMBO_COLUMN_WIDTH;
        public int CONTROL_MARGIN = 7;
        public int CONTROL_MARGIN_CENTRAL = 3;
        public int SCROLL_BAR_Width = 20;
        public int SCROLL_BAR_Height = 16;
        public int BUTTON_Width = 50; // 75;
        public int BUTTON_Height = 20;

        public ConstantsForDesigner(bool iOS = false)
        {
            if (iOS)
            {
                LABEL_COLUMN_WIDTH = 350;
                ROW_HEIGHT = 38;
                CURRENCY_COLUMN_WIDTH = 129;
                CODE_COLUMN_WIDTH = 50;
                LEVEL_TAB = 10;
                LABEL_Width = 125;
                LABEL_Height = 21;
                CURRENCY_Width = 121;
                CURRENCY_Height = 30;
                COMBO_Width = 190;
                COMBO_Height = 30;
            }
            USER_COMBO_COLUMN_WIDTH = USER_COMBO_Width + CONTROL_MARGIN;
            COMBO_COLUMN_WIDTH = COMBO_Width + CONTROL_MARGIN;
        }

        public bool DynamicRowHeight = true;
    }
}
