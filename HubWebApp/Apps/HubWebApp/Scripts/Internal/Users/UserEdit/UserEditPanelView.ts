import { CardTitleHeaderView, CardView } from "@jasonbenfield/sharedwebapp/Views/Card";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/Views/MessageAlertView";
import { CssLengthUnit } from "../../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/CssLengthUnit";
import { BasicComponentView } from "../../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/BasicComponentView";
import { ButtonCommandView } from "../../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/Commands";
import { GridView } from "../../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/Grid";
import { ToolbarView } from "../../../../../../../../SharedWebApp/Apps/SharedWebApp/Scripts/Lib/Views/ToolbarView";
import { EditUserFormView } from "../../../Lib/Api/EditUserFormView";
import { HubTheme } from "../../HubTheme";

export class UserEditPanelView extends GridView {
    readonly alert: MessageAlertView;
    readonly editUserForm: EditUserFormView;
    readonly titleHeader: CardTitleHeaderView;
    readonly cancelButton: ButtonCommandView;
    readonly saveButton: ButtonCommandView;

    constructor(container: BasicComponentView) {
        super(container);
        this.height100();
        this.setViewName(UserEditPanelView.name);
        this.layout();
        this.setTemplateRows(CssLengthUnit.flex(1), CssLengthUnit.auto());
        const mainContent = HubTheme.instance.mainContent(this.addCell());
        this.alert = mainContent.addView(MessageAlertView);
        const toolbar = this.addView(ToolbarView);
        HubTheme.instance.commandToolbar.toolbar(toolbar);
        this.cancelButton = toolbar.columnEnd.addView(ButtonCommandView);
        HubTheme.instance.commandToolbar.cancelButton(this.cancelButton);
        this.saveButton = toolbar.columnEnd.addView(ButtonCommandView);
        HubTheme.instance.commandToolbar.saveButton(this.saveButton);
        const editCard = mainContent.addView(CardView);
        this.titleHeader = editCard.addCardTitleHeader();
        let cardBody = editCard.addCardBody();
        this.editUserForm = cardBody.addView(EditUserFormView);
        this.editUserForm.addContent();
        this.editUserForm.addOffscreenSubmit();
    }
}