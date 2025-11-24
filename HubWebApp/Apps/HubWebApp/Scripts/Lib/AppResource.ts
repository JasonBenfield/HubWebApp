import { AppResourceName } from "./AppResourceName";
import { ResourceResultType } from "./Http/ResourceResultType";

export class AppResource {
    readonly id: number;
    readonly name: AppResourceName;
    readonly isAnonymousAllowed: boolean;
    readonly resultType: ResourceResultType;

    constructor(source?: IResourceModel) {
        this.id = source ? source.ID : 0;
        this.name = new AppResourceName(source && source.Name);
        this.isAnonymousAllowed = source ? source.IsAnonymousAllowed : false;
        this.resultType = source ? ResourceResultType.values.value(source.ResultType) : ResourceResultType.values.None;
    }
}