import { DefaultEvent } from "XtiShared/Events";
import { Command } from "XtiShared/Command/Command";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { Card } from "XtiShared/Card/Card";
import { BlockViewModel } from "XtiShared/Html/BlockViewModel";
import { MessageAlert } from "XtiShared/MessageAlert";
import { TextSpan } from "XtiShared/Html/TextSpan";
import { Row } from "XtiShared/Grid/Row";
import { ColumnCss } from "XtiShared/ColumnCss";
import { ButtonCommandItem } from "XtiShared/Command/ButtonCommandItem";
import { ContextualClass } from "XtiShared/ContextualClass";
import { FormGroup } from "XtiShared/Html/FormGroup";
import { CardBody } from "XtiShared/Card/CardBody";
import { TextCss } from "XtiShared/TextCss";
import { HubTheme } from "../../HubTheme";

export class UserComponent extends Card {
    constructor(
        private readonly hubApi: HubAppApi,
        vm: BlockViewModel = new BlockViewModel()
    ) {
        super(vm);
        this.setName(UserComponent.name);
        let headerRow = this.addCardHeader()
            .addContent(new Row());
        headerRow.addColumn()
            .addContent(new TextSpan('User'));
        let editButton = this.editCommand.add(
            headerRow.addColumn()
                .configure(c => c.setColumnCss(ColumnCss.xs('auto')))
                .addContent(HubTheme.instance.cardHeader.editButton())
        );
        this.alert = this.addCardAlert().alert;
        let body = this.addCardBody();
        this.userName = this.addBodyRow(body, 'User Name');
        this.fullName = this.addBodyRow(body, 'Name');
        this.email = this.addBodyRow(body, 'Email');
    }

    private addBodyRow(body: CardBody, caption: string) {
        let row = body.addContent(new FormGroup());
        row.captionColumn.addContent(new TextSpan(caption));
        row.captionColumn.setColumnCss(ColumnCss.xs(4));
        row.valueColumn.setTextCss(new TextCss().truncate());
        return row.valueColumn.addContent(new TextSpan())
            .configure(ts => ts.addCssName('form-control-plaintext'));
    }

    private userID: number;

    setUserID(userID: number) {
        this.userID = userID;
    }

    private readonly _editRequested = new DefaultEvent<number>(this);
    readonly editRequested = this._editRequested.handler();

    private readonly editCommand = new Command(this.requestEdit.bind(this));

    private requestEdit() {
        this._editRequested.invoke(this.userID);
    }

    private readonly alert: MessageAlert;

    private readonly userName: TextSpan;
    private readonly fullName: TextSpan;
    private readonly email: TextSpan;

    async refresh() {
        let user = await this.getUser(this.userID);
        this.userName.setText(user.UserName);
        this.userName.setTitleFromText();
        this.fullName.setText(user.Name);
        this.fullName.setTitleFromText();
        this.email.setText(user.Email);
        this.email.setTitleFromText();
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