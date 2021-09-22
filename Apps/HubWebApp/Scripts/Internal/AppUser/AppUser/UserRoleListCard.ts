import { AlignCss } from "XtiShared/AlignCss";
import { Card } from "XtiShared/Card/Card";
import { CardAlert } from "XtiShared/Card/CardAlert";
import { CardHeader } from "XtiShared/Card/CardHeader";
import { CardListGroup } from "XtiShared/Card/CardListGroup";
import { ColumnCss } from "XtiShared/ColumnCss";
import { Command } from "XtiShared/Command/Command";
import { SimpleEvent } from "XtiShared/Events";
import { Row } from "XtiShared/Grid/Row";
import { BlockViewModel } from "XtiShared/Html/BlockViewModel";
import { TextSpan } from "XtiShared/Html/TextSpan";
import { MessageAlert } from "XtiShared/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { HubTheme } from "../../HubTheme";

export class UserRoleListCard extends Card {
    constructor(
        private readonly hubApi: HubAppApi,
        vm: BlockViewModel = new BlockViewModel()
    ) {
        super(vm);
        let row = this.addContent(new CardHeader())
            .addContent(new Row())
            .configure(r => r.addCssFrom(new AlignCss().items(a => a.xs('baseline')).cssClass()));
        row.addColumn()
            .addContent(new TextSpan('User Roles'));
        let editCommand = row.addColumn()
            .configure(col => col.setColumnCss(ColumnCss.xs('auto')))
            .addContent(HubTheme.instance.cardHeader.editButton());
        this.editCommand.add(editCommand);
        this.alert = this.addContent(new CardAlert()).alert;
        this.roles = this.addContent(new CardListGroup());
    }

    private readonly _editRequested = new SimpleEvent(this);
    readonly editRequested = this._editRequested.handler();

    private readonly editCommand = new Command(this.requestEdit.bind(this));

    private requestEdit() {
        this._editRequested.invoke();
    }

    private readonly alert: MessageAlert;
    private readonly roles: CardListGroup;

    private userID: number;

    setUserID(userID: number) {
        this.userID = userID;
    }

    async refresh() {
        let roles = await this.getRoles();
        this.roles.setItems(
            roles,
            (role, listItem) => {
                listItem.addContent(new Row())
                    .addColumn()
                    .addContent(new TextSpan(role.Name));
            }
        );
        if (roles.length === 0) {
            this.alert.danger('No Roles were Found for User');
        }
    }

    private async getRoles() {
        let roles: IAppRoleModel[];
        await this.alert.infoAction(
            'Loading...',
            async () => {
                roles = await this.hubApi.AppUser.GetUserRoles({
                    UserID: this.userID,
                    ModifierID: 0
                });
            }
        );
        return roles;
    }
}