import { App } from "../../Lib/App";
import { AppUser } from "../../Lib/AppUser";
import { Modifier } from "../../Lib/Modifier";

export class AppUserOptions {
    constructor(
        readonly app: App,
        readonly user: AppUser,
        readonly defaultModifier: Modifier
    ) {
    }
}