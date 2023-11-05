import { BasicComponent } from "@jasonbenfield/sharedwebapp/Components/BasicComponent";
import { CardAlert } from "@jasonbenfield/sharedwebapp/Components/CardAlert";
import { ListGroup } from "@jasonbenfield/sharedwebapp/Components/ListGroup";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { TextComponent } from "@jasonbenfield/sharedwebapp/Components/TextComponent";
import { TextListGroupItemView } from "@jasonbenfield/sharedwebapp/Views/ListGroup";
import { HubAppClient } from "../../Lib/Http/HubAppClient";
import { UserAuthenticatorListCardView } from "./UserAuthenticatorListCardView";

export class UserAuthenticatorListCard extends BasicComponent {
    private readonly alert: MessageAlert;
    private readonly userAuthenticators: ListGroup<TextComponent, TextListGroupItemView>;
    private userID: number;

    constructor(private readonly hubClient: HubAppClient, protected readonly view: UserAuthenticatorListCardView) {
        super(view);
        this.alert = new CardAlert(view.alert).alert;
        this.userAuthenticators = new ListGroup(view.userAuthenticators);
    }

    setUserID(userID: number) {
        this.userID = userID;
    }

    async refresh() {
        const userAuthenticators = await this.alert.infoAction(
            'Loading...',
            () => this.hubClient.UserInquiry.GetUserAuthenticators(this.userID)
        );
        this.userAuthenticators.setItems(
            userAuthenticators,
            (userAuth, itemView) => {
                const item = new TextComponent(itemView);
                item.setText(userAuth.Authenticator.AuthenticatorKey.DisplayText);
                return item;
            }
        );
        if (userAuthenticators.length === 0) {
            this.alert.warning('No authenticators have been added.');
        }
    }
}