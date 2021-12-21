import { CardTitleHeader } from "@jasonbenfield/sharedwebapp/Card/CardTitleHeader";
import { SimpleEvent } from "@jasonbenfield/sharedwebapp/Events";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { UserModCategoryListCardView } from "./UserModCategoryListCardView";

export class UserModCategoryListCard {
    private readonly alert: MessageAlert;
    private userID: number;

    private readonly _editRequested = new SimpleEvent(this);
    readonly editRequested = this._editRequested.handler();

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: UserModCategoryListCardView
    ) {
        new CardTitleHeader('User Modifiers', this.view.titleHeader);
        this.alert = new MessageAlert(this.view.alert);
    }

    private requestEdit(userModCategory: IUserModifierCategoryModel) {
        this._editRequested.invoke();
    }

    setUserID(userID: number) {
        this.userID = userID;
    }

    async refresh() {
        let userModCategories = await this.getUserModCategories();
        for (let userModCategory of userModCategories) {
            //    let header = this.addCardHeader();
            //    let headerRow = header.addContent(new Row())
            //        .configure(row => row.addCssFrom(new AlignCss().items(a => a.xs('baseline')).cssClass()));
            //    headerRow.addColumn()
            //        .addContent(new TextSpan(userModCategory.ModCategory.Name));
            //    let editButton = headerRow.addColumn()
            //        .configure(col => col.setColumnCss(ColumnCss.xs('auto')))
            //        .addContent(HubTheme.instance.cardHeader.editButton());
            //    let editCommand = new Command(this.requestEdit.bind(this, userModCategory));
            //    editCommand.add(editButton);
            //    this.modCategoryComponents.push(header);
            //    let listGroup = this.addListGroup();
            //    this.modCategoryComponents.push(listGroup);
            //    listGroup.setItems(
            //        userModCategory.Modifiers,
            //        (modifier, listItem) => {
            //            let row = listItem.addContent(new Row());
            //            row
            //                .addColumn()
            //                .configure(c => {
            //                    c.setColumnCss(ColumnCss.xs(4));
            //                    c.addCssFrom(new TextCss().truncate().cssClass());
            //                })
            //                .addContent(new TextSpan(modifier.ModKey))
            //                .configure(ts => ts.setTitleFromText());
            //            row
            //                .addColumn()
            //                .configure(c => {
            //                    c.addCssFrom(new TextCss().truncate().cssClass());
            //                })
            //                .addContent(new TextSpan(modifier.DisplayText))
            //                .configure(ts => ts.setTitleFromText());
            //        }
            //    );
            //    if (userModCategory.Modifiers.length === 0) {
            //        let cardAlert = this.addCardAlert();
            //        this.modCategoryComponents.push(cardAlert);
            //        cardAlert.alert.danger('No Modifiers were Found for User');
            //    }
            //}
            //if (userModCategories.length === 0) {
            //    this.alert.danger('No Modifiers were found');
            //}
        }
    }

    private async getUserModCategories() {
        let modCategories: IUserModifierCategoryModel[];
        await this.alert.infoAction(
            'Loading...',
            async () => {
                modCategories = await this.hubApi.AppUser.GetUserModCategories(this.userID);
            }
        );
        return modCategories;
    }
}