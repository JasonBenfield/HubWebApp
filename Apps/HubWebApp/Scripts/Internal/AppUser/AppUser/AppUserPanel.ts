import { Awaitable } from "@jasonbenfield/sharedwebapp/Awaitable";
import { Command } from "@jasonbenfield/sharedwebapp/Command/Command";
import { HubAppApi } from "../../../Hub/Api/HubAppApi";
import { AppUserPanelView } from "./AppUserPanelView";
import { UserComponent } from "./UserComponent";
import { UserModCategoryListCard } from "./UserModCategoryListCard";
import { UserRoleListCard } from "./UserRoleListCard";

interface Results {
    backRequested?: {};
    editUserRolesRequested?: {};
    editUserModCategoryRequested?: { userModCategory: IUserModifierCategoryModel; };
}

export class AppUserPanelResult {
    static get backRequested() {
        return new AppUserPanelResult({ backRequested: {} });
    }

    static get editUserRolesRequested() {
        return new AppUserPanelResult({ editUserRolesRequested: {} });
    }

    static editUserModCategoryRequested(userModCategory: IUserModifierCategoryModel) {
        return new AppUserPanelResult({
            editUserModCategoryRequested: { userModCategory: userModCategory }
        });
    }

    private constructor(private readonly results: Results) { }

    get backRequested() { return this.results.backRequested; }

    get editUserRolesRequested() { return this.results.editUserRolesRequested; }

    get editUserModCategoryRequested() { return this.results.editUserModCategoryRequested; }
}

export class AppUserPanel implements IPanel {
    private readonly userComponent: UserComponent;
    private readonly userRoles: UserRoleListCard;
    private readonly userModCategories: UserModCategoryListCard;
    private readonly awaitable = new Awaitable<AppUserPanelResult>();
    private readonly backCommand = new Command(this.back.bind(this));

    constructor(
        private readonly hubApi: HubAppApi,
        private readonly view: AppUserPanelView
    ) {
        this.userComponent = new UserComponent(this.hubApi, this.view.userComponent);
        this.userRoles = new UserRoleListCard(this.hubApi, this.view.userRoles);
        this.userRoles.editRequested.register(this.onEditUserRolesRequested.bind(this));
        this.userModCategories = new UserModCategoryListCard(this.hubApi, this.view.userModCategories);
        this.userModCategories.editRequested.register(this.onEditUserModCategoryRequested.bind(this));
        this.backCommand.add(this.view.backButton);
    }

    private onEditUserRolesRequested() {
        this.awaitable.resolve(AppUserPanelResult.editUserRolesRequested);
    }

    private onEditUserModCategoryRequested(userModCategory: IUserModifierCategoryModel) {
        this.awaitable.resolve(
            AppUserPanelResult.editUserModCategoryRequested(userModCategory)
        );
    }

    setUserID(userID: number) {
        this.userComponent.setUserID(userID);
        this.userRoles.setUserID(userID);
        this.userModCategories.setUserID(userID);
    }

    refresh() {
        let promises: Promise<any>[] = [
            this.userComponent.refresh(),
            this.userRoles.refresh(),
            this.userModCategories.refresh()
        ];
        return Promise.all(promises);
    }

    start() {
        return this.awaitable.start();
    }

    private back() {
        this.awaitable.resolve(
            AppUserPanelResult.backRequested
        );
    }

    activate() { this.view.show(); }

    deactivate() { this.view.hide(); }
}