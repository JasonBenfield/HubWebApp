import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { DefaultEvent } from "@jasonbenfield/sharedwebapp/Events";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { HubAppApi } from "../../../Lib/Api/HubAppApi";
import { UserListCardView } from "./UserListCardView";
import { UserListItem } from "./UserListItem";
import { UserListItemView } from "./UserListItemView";

export class UserListCard {
    private readonly alert: MessageAlert;
    private readonly users: ListGroup;

    private readonly _userSelected = new DefaultEvent<IAppUserModel>(this);
    readonly userSelected = this._userSelected.handler();

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: UserListCardView
    ) {
        new TextComponent(this.view.titleHeader).setText('Users');
        this.alert = new CardAlert(this.view.alert).alert;
        this.users = new ListGroup(this.view.users);
        this.users.registerItemClicked(this.onUserClicked.bind(this));
    }

    private onUserClicked(listItem: UserListItem) {
        this._userSelected.invoke(listItem.user);
    }

    async refresh() {
        let users = await this.getUsers();
        this.users.setItems(
            users,
            (user: IAppUserModel, listItem: UserListItemView) =>
                new UserListItem(user, listItem)
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