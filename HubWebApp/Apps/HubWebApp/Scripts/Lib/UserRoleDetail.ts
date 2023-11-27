import { App } from "./App";
import { AppRole } from "./AppRole";
import { AppUser } from "./AppUser";
import { Modifier } from "./Modifier";
import { ModifierCategory } from "./ModifierCategory";

export class UserRoleDetail {
	readonly id: number;
	readonly user: AppUser;
	readonly app: App;
	readonly role: AppRole;
	readonly modCategory: ModifierCategory;
	readonly modifier: Modifier;

	constructor(readonly source: IUserRoleDetailModel) {
		this.id = source.ID;
		this.user = new AppUser(source.User);
		this.app = new App(source.App);
		this.role = new AppRole(source.Role);
		this.modCategory = new ModifierCategory(source.ModCategory);
		this.modifier = new Modifier(source.Modifier);
    }
}