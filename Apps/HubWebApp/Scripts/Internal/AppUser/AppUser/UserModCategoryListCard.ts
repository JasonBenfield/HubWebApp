import { Row } from "XtiShared/Grid/Row";
import { TextSpan } from "XtiShared/Html/TextSpan";
import { MessageAlert } from "XtiShared/MessageAlert";
import { CardAlert } from "XtiShared/Card/CardAlert";
import { AlignCss } from "XtiShared/AlignCss";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { CardHeader } from "XtiShared/Card/CardHeader";
import { ColumnCss } from "XtiShared/ColumnCss";
import { SimpleEvent } from "XtiShared/Events";
import { HubTheme } from "../../HubTheme";
import { Command } from "XtiShared/Command/Command";
import { Card } from "XtiShared/Card/Card";
import { BlockViewModel } from "XtiShared/Html/BlockViewModel";
import { TextCss } from "XtiShared/TextCss";
import { CardBody } from "XtiShared/Card/CardBody";
import { Heading5 } from "XtiShared/Html/Heading5";
import { ContextualClass } from "../../../../Imports/Shared/ContextualClass";

export class UserModCategoryListCard extends Card {
    constructor(
        private readonly hubApi: HubAppApi,
        vm: BlockViewModel = new BlockViewModel()
    ) {
        super(vm);
        this.addCardTitleHeader('User Modifiers');
        this.alert = this.addContent(new CardAlert()).alert;
    }

    private readonly _editRequested = new SimpleEvent(this);
    readonly editRequested = this._editRequested.handler();

    private requestEdit(userModCategory: IUserModifierCategoryModel) {
        this._editRequested.invoke();
    }

    private readonly alert: MessageAlert;

    private readonly modCategoryComponents: IComponent[] = [];

    private userID: number;

    setUserID(userID: number) {
        this.userID = userID;
    }

    async refresh() {
        this.removeModCategories();
        let userModCategories = await this.getUserModCategories();
        for (let userModCategory of userModCategories) {
            let header = this.addCardHeader();
            let headerRow = header.addContent(new Row())
                .configure(row => row.addCssFrom(new AlignCss().items(a => a.xs('baseline')).cssClass()));
            headerRow.addColumn()
                .addContent(new TextSpan(userModCategory.ModCategory.Name));
            let editButton = headerRow.addColumn()
                .configure(col => col.setColumnCss(ColumnCss.xs('auto')))
                .addContent(HubTheme.instance.cardHeader.editButton());
            let editCommand = new Command(this.requestEdit.bind(this, userModCategory));
            editCommand.add(editButton);
            this.modCategoryComponents.push(header);
            let listGroup = this.addListGroup();
            this.modCategoryComponents.push(listGroup);
            if (userModCategory.HasAccessToAll) {
                let listItem = listGroup.addItem();
                let row = listItem.addContent(new Row());
                row
                    .addColumn()
                    .addContent(new TextSpan('Has Access to All Modifiers'))
                    .configure(ts => ts.setTextCss(new TextCss().context(ContextualClass.success)));
            }
            else {
                listGroup.setItems(
                    userModCategory.Modifiers,
                    (modifier, listItem) => {
                        let row = listItem.addContent(new Row());
                        row
                            .addColumn()
                            .configure(c => {
                                c.setColumnCss(ColumnCss.xs(4));
                                c.addCssFrom(new TextCss().truncate().cssClass());
                            })
                            .addContent(new TextSpan(modifier.ModKey))
                            .configure(ts => ts.setTitleFromText());
                        row
                            .addColumn()
                            .configure(c => {
                                c.addCssFrom(new TextCss().truncate().cssClass());
                            })
                            .addContent(new TextSpan(modifier.DisplayText))
                            .configure(ts => ts.setTitleFromText());
                    }
                );
                if (userModCategory.Modifiers.length === 0) {
                    let cardAlert = this.addCardAlert();
                    this.modCategoryComponents.push(cardAlert);
                    cardAlert.alert.danger('No Modifiers were Found for User');
                }
            }
        }
        if (userModCategories.length === 0) {
            this.alert.danger('No Modifiers were found');
        }
    }

    private removeModCategories() {
        for (let modCategory of this.modCategoryComponents) {
            this.removeItem(modCategory);
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