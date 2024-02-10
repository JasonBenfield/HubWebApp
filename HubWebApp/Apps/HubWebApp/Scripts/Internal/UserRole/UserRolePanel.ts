import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { AsyncCommand, Command } from "@jasonbenfield/sharedwebapp/Components/Command";
import { MessageAlert } from "@jasonbenfield/sharedwebapp/Components/MessageAlert";
import { FormGroupContainer } from "@jasonbenfield/sharedwebapp/Forms/FormGroupContainer";
import { FormGroupText } from "@jasonbenfield/sharedwebapp/Forms/FormGroupText";
import { UserRoleDetail } from "../../Lib/UserRoleDetail";
import { HubAppClient } from "../../Lib/Http/HubAppClient";
import { UserRolePanelView } from "./UserRolePanelView";
import { LinkComponent } from "@jasonbenfield/sharedwebapp/Components/LinkComponent";
import { ModalConfirm } from "@jasonbenfield/sharedwebapp/Components/ModalConfirm";
import { ModalMessageAlert } from "@jasonbenfield/sharedwebapp/Components/ModalMessageAlert";

interface IResult {
    menuRequested?: boolean;
}

class Result {
    static menuRequested() { return new Result({ menuRequested: true }); }

    private constructor(private readonly result: IResult) { }

    get menuRequested() { return this.result.menuRequested; }
}

export class UserRolePanel implements IPanel {
    private readonly awaitable = new Awaitable<Result>();
    private readonly alert: MessageAlert;
    private readonly formGroups: FormGroupContainer;
    private readonly userNameFormGroup: FormGroupText;
    private readonly appKeyFormGroup: FormGroupText;
    private readonly modCategoryNameFormGroup: FormGroupText;
    private readonly modifierNameFormGroup: FormGroupText;
    private readonly roleNameFormGroup: FormGroupText;
    private readonly viewAppLink: LinkComponent;
    private readonly viewUserLink: LinkComponent;
    private readonly deleteCommand: AsyncCommand;
    private readonly modalAlert: ModalMessageAlert;
    private readonly modalConfirm: ModalConfirm;
    private userRoleDetail: UserRoleDetail;
    private userRoleID: number;

    constructor(private readonly hubClient: HubAppClient, private readonly view: UserRolePanelView) {
        this.alert = new MessageAlert(view.alert);
        this.formGroups = new FormGroupContainer(view.formGroupContainerView);
        this.userNameFormGroup = this.formGroups.addFormGroupText(view.userNameFormGroupView);
        this.userNameFormGroup.setCaption('User Name');
        this.appKeyFormGroup = this.formGroups.addFormGroupText(view.appKeyFormGroupView);
        this.appKeyFormGroup.setCaption('App');
        this.modCategoryNameFormGroup = this.formGroups.addFormGroupText(view.modCategoryNameFormGroupView);
        this.modCategoryNameFormGroup.setCaption('Mod Category');
        this.modifierNameFormGroup = this.formGroups.addFormGroupText(view.modifierNameFormGroupView);
        this.modifierNameFormGroup.setCaption('Modifier');
        this.roleNameFormGroup = this.formGroups.addFormGroupText(view.roleNameFormGroupView);
        this.roleNameFormGroup.setCaption('Role');
        this.viewAppLink = new LinkComponent(view.appLink);
        this.viewUserLink = new LinkComponent(view.userLink);
        this.deleteCommand = new AsyncCommand(this.deleteUserRole.bind(this));
        this.deleteCommand.add(view.deleteUserRoleLink);
        this.modalConfirm = new ModalConfirm(view.modalConfirmView);
        this.modalAlert = new ModalMessageAlert(view.modalAlertView);
        new Command(this.menu.bind(this)).add(view.menuButton);
        this.hideComponents();
    }

    private menu() { this.awaitable.resolve(Result.menuRequested()); }

    private async deleteUserRole() {
        const isConfirmed = await this.modalConfirm.confirm(
            `Delete user role '${this.userRoleDetail.role.name.displayText}'?`,
            'Confirm Delete'
        );
        if (isConfirmed) {
            await this.alert.infoAction(
                'Deleting...',
                () => this.hubClient.UserRoles.DeleteUserRole({ UserRoleID: this.userRoleID })
            );
            await this.modalAlert.alert(
                a => {
                    a.success('User Role has been deleted.');
                }
            );
            this.hubClient.UserRoles.Index.open({ AppID: null });
        }
    }
    
    setUserRoleID(userRoleID: number) {
        this.userRoleID = userRoleID;
    }

    private hideComponents() {
        this.userNameFormGroup.hide();
        this.appKeyFormGroup.hide();
        this.modCategoryNameFormGroup.hide();
        this.modifierNameFormGroup.hide();
        this.roleNameFormGroup.hide();
        this.viewAppLink.hide();
        this.viewUserLink.hide();
    }

    async refresh() {
        this.hideComponents();
        const sourceDetail = await this.alert.infoAction(
            'Loading...',
            () => this.hubClient.UserRoles.GetUserRoleDetail({ UserRoleID: this.userRoleID })
        );
        const detail = new UserRoleDetail(sourceDetail);
        this.userRoleDetail = detail;
        this.userNameFormGroup.show();
        this.userNameFormGroup.setValue(detail.user.userName.displayText);
        this.appKeyFormGroup.show();
        this.appKeyFormGroup.setValue(detail.app.appKey.format());
        if (!detail.modCategory.isDefault) {
            this.modCategoryNameFormGroup.show();
            this.modCategoryNameFormGroup.setValue(detail.modCategory.name.displayText);
        }
        if (!detail.modifier.isDefault) {
            this.modifierNameFormGroup.show();
            this.modifierNameFormGroup.setValue(detail.modifier.displayText);
        }
        this.roleNameFormGroup.show();
        this.roleNameFormGroup.setValue(detail.role.name.displayText);
        this.viewAppLink.setHref(
            this.hubClient.App.Index.getModifierUrl(detail.app.getModifier(), {})
        );
        this.viewAppLink.show();
        this.viewUserLink.setHref(
            this.hubClient.Users.Index.getModifierUrl(
                detail.userGroup.getModifier(),
                {
                    UserID: detail.user.id,
                    ReturnTo: null
                }
            )
        );
        this.viewUserLink.show();
    }

    start() { return this.awaitable.start(); }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }

}