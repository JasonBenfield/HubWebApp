import { Awaitable } from "XtiShared/Awaitable";
import { Command } from "XtiShared/Command/Command";
import { Result } from "XtiShared/Result";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { EditUserForm } from '../../../Hub/Api/EditUserForm';
import { DelayedAction } from 'XtiShared/DelayedAction';
import { AsyncCommand } from "XtiShared/Command/AsyncCommand";
import { BlockViewModel } from "XtiShared/Html/BlockViewModel";
import { Block } from "XtiShared/Html/Block";
import { FlexColumn } from "XtiShared/Html/FlexColumn";
import { FlexColumnFill } from "XtiShared/Html/FlexColumnFill";
import { MessageAlert } from "XtiShared/MessageAlert";
import { HubTheme } from "../../HubTheme";
import { Card } from "XtiShared/Card/Card";
import { TextCss } from "XtiShared/TextCss";

export class UserEditPanel extends Block {
    public static readonly ResultKeys = {
        canceled: 'canceled',
        saved: 'saved'
    };

    constructor(
        private readonly hubApi: HubAppApi,
        vm: BlockViewModel = new BlockViewModel()
    ) {
        super(vm);
        this.height100();
        this.setName(UserEditPanel.name);
        let flexColumn = this.addContent(new FlexColumn());
        let flexFill = flexColumn.addContent(new FlexColumnFill());
        this.alert = flexFill.container.addContent(new MessageAlert());

        let toolbar = flexColumn.addContent(HubTheme.instance.commandToolbar.toolbar());
        this.cancelCommand.add(
            toolbar.columnEnd.addContent(HubTheme.instance.commandToolbar.cancelButton())
        );
        this.saveCommand.add(
            toolbar.columnEnd.addContent(HubTheme.instance.commandToolbar.saveButton())
        );
        let editCard = flexFill.addContent(new Card());
        editCard.addCardTitleHeader('Edit User');
        let cardBody = editCard.addCardBody();
        this.editUserForm = cardBody.addContent(new EditUserForm());
        this.editUserForm.addOffscreenSubmit();
        this.editUserForm.executeLayout();
        this.editUserForm.forEachFormGroup(fg => {
            fg.captionColumn.setTextCss(new TextCss().end().bold());
        });
    }

    private readonly alert: MessageAlert;
    private readonly editUserForm: EditUserForm;

    private userID: number;

    setUserID(userID: number) {
        this.userID = userID;
    }

    async refresh() {
        let userForm = await this.getUserForEdit(this.userID);
        this.editUserForm.import(userForm);
        await new DelayedAction(
            () => this.editUserForm.PersonName.setFocus(),
            1000
        ).execute();
    }

    private async getUserForEdit(userID: number) {
        let userForm: Record<string, object>;
        await this.alert.infoAction(
            'Loading...',
            async () => {
                userForm = await this.hubApi.UserMaintenance.GetUserForEdit(userID);
            }
        );
        return userForm;
    }

    private readonly awaitable = new Awaitable();

    start() {
        return this.awaitable.start();
    }

    private readonly cancelCommand = new Command(this.cancel.bind(this));

    private cancel() {
        this.awaitable.resolve(
            new Result(UserEditPanel.ResultKeys.canceled)
        );
    }

    private readonly saveCommand = new AsyncCommand(this.save.bind(this));

    private async save() {
        let result = await this.editUserForm.save(this.hubApi.UserMaintenance.EditUserAction);
        if (result.succeeded()) {
            this.awaitable.resolve(
                new Result(UserEditPanel.ResultKeys.saved)
            );
        }
    }
}