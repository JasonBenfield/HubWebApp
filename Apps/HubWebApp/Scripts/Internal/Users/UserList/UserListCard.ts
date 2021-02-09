import { DefaultEvent } from "XtiShared/Events";
import { Card } from "XtiShared/Card/Card";
import { CardButtonListGroup } from "XtiShared/Card/CardButtonListGroup";
import { ColumnCss } from "XtiShared/ColumnCss";
import { Row } from "XtiShared/Grid/Row";
import { BlockViewModel } from "XtiShared/Html/BlockViewModel";
import { TextSpan } from "XtiShared/Html/TextSpan";
import { MessageAlert } from "XtiShared/MessageAlert";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";

export class UserListCard extends Card {
    constructor(
        private readonly hubApi: HubAppApi,
        vm: BlockViewModel = new BlockViewModel()
    ) {
        super(vm);
        this.setName(UserListCard.name);
        this.addCardTitleHeader('Users');
        this.alert = this.addCardAlert().alert;
        this.users = this.addButtonListGroup();
        this.users.itemClicked.register(this.onUserClicked.bind(this));
    }

    private onUserClicked(listItem: IListItem) {
        this._userSelected.invoke(listItem.getData<IAppUserModel>());
    }

    private readonly alert: MessageAlert;
    private readonly users: CardButtonListGroup;

    private readonly _userSelected = new DefaultEvent<IAppUserModel>(this);
    readonly userSelected = this._userSelected.handler();

    async refresh() {
        let users = await this.getUsers();
        this.users.setItems(
            users,
            (user, listItem) => {
                listItem.setData(user);
                let row = listItem.addContent(new Row());
                row.addColumn()
                    .configure(c => c.setColumnCss(ColumnCss.xs(4)))
                    .addContent(new TextSpan(user.UserName));
                row.addColumn()
                    .addContent(new TextSpan(user.Name));
            }
        );
        if (users.length === 0) {
            this.alert.danger('No Users were Found');
        }
    }

    private async getUsers() {
        let users: IAppUserModel[];
        await this.alert.infoAction(
            'Loading...',
            async () => {
                users = await this.hubApi.Users.GetUsers();
            }
        );
        return users;
    }
}