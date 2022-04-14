
export class AppUserOptions {
    constructor(
        readonly app: IAppModel,
        readonly user: IAppUserModel,
        readonly defaultModifier: IModifierModel
    ) {
    }
}