import { MessageAlert } from "XtiShared/MessageAlert";
import { CardAlert } from "XtiShared/Card/CardAlert";
import { ColumnCss } from "XtiShared/ColumnCss";
import { Row } from "XtiShared/Grid/Row";
import { TextSpan } from "XtiShared/Html/TextSpan";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { CardTitleHeader } from "XtiShared/Card/CardTitleHeader";
import { CardBody } from "XtiShared/Card/CardBody";
import { Card } from "XtiShared/Card/Card";
import { BlockViewModel } from "XtiShared/Html/BlockViewModel";

export class UserComponent extends Card {
    constructor(
        private readonly hubApi: HubAppApi,
        vm: BlockViewModel = new BlockViewModel()
    ) {
        super(vm);
        this.addContent(new CardTitleHeader('User'));
        this.alert = this.addContent(new CardAlert()).alert;
        this.cardBody = this.addContent(new CardBody());
        let row = this.cardBody.addContent(new Row());
        this.userName = row.addColumn()
            .configure(c => c.setColumnCss(ColumnCss.xs('auto')))
            .addContent(new TextSpan('User'));
        this.fullName = row.addColumn()
            .addContent(new TextSpan());
        this.cardBody.hide();
    }

    private readonly cardBody: CardBody;
    private readonly userName: TextSpan;
    private readonly fullName: TextSpan;

    private userID: number;

    setUserID(userID) {
        this.userID = userID;
    }

    private readonly alert: MessageAlert;

    async refresh() {
        let user = await this.getUser(this.userID);
        this.userName.setText(user.UserName);
        this.fullName.setText(user.Name);
        this.cardBody.show();
    }

    private async getUser(userID: number) {
        let user: IAppUserModel;
        await this.alert.infoAction(
            'Loading...',
            async () => {
                user = await this.hubApi.UserInquiry.GetUser(userID);
            }
        );
        return user;
    }
}