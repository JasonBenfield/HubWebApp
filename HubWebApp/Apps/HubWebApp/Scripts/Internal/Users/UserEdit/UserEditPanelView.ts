import { CardTitleHeaderView } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeaderView";
import { CardView } from "@jasonbenfield/sharedwebapp/Card/CardView";
import { ColumnCss } from "@jasonbenfield/sharedwebapp/ColumnCss";
import { ButtonCommandItem } from "@jasonbenfield/sharedwebapp/Command/ButtonCommandItem";
import { Block } from "@jasonbenfield/sharedwebapp/Html/Block";
import { FlexColumn } from "@jasonbenfield/sharedwebapp/Html/FlexColumn";
import { FlexColumnFill } from "@jasonbenfield/sharedwebapp/Html/FlexColumnFill";
import { MessageAlertView } from "@jasonbenfield/sharedwebapp/MessageAlertView";
import { TextCss } from "@jasonbenfield/sharedwebapp/TextCss";
import { EditUserFormView } from "../../../Hub/Api/EditUserFormView";
import { HubTheme } from "../../HubTheme";

export class UserEditPanelView extends Block {
    readonly alert: MessageAlertView;
    readonly editUserForm: EditUserFormView;
    readonly titleHeader: CardTitleHeaderView;
    readonly cancelButton: ButtonCommandItem;
    readonly saveButton: ButtonCommandItem;

    constructor() {
        super();
        this.height100();
        this.setName(UserEditPanelView.name);
        let flexColumn = this.addContent(new FlexColumn());
        let flexFill = flexColumn.addContent(new FlexColumnFill());
        this.alert = flexFill.container.addContent(new MessageAlertView());
        let toolbar = flexColumn.addContent(HubTheme.instance.commandToolbar.toolbar());
        this.cancelButton = toolbar.columnEnd.addContent(
            HubTheme.instance.commandToolbar.cancelButton()
        );
        this.saveButton = toolbar.columnEnd.addContent(
            HubTheme.instance.commandToolbar.saveButton()
        );
        let editCard = flexFill.addContent(new CardView());
        this.titleHeader = editCard.addCardTitleHeader();
        let cardBody = editCard.addCardBody();
        this.editUserForm = cardBody.addContent(new EditUserFormView());
        this.editUserForm.addOffscreenSubmit();
        this.editUserForm.executeLayout();
        this.editUserForm.forEachFormGroup(fg => {
            fg.captionColumn.setColumnCss(ColumnCss.xs(4));
            fg.captionColumn.setTextCss(new TextCss().end());
        });
    }
}