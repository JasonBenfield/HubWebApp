import { ResourceResultType } from "./Http/ResourceResultType";
import { AppResourceName } from "./AppResourceName";

export class AppResource {
	readonly id: number;
	readonly name: AppResourceName;
	readonly isAnonymousAllowed: boolean;
	readonly resultType: ResourceResultType;

	constructor(source: IResourceModel) {
		this.id = source.ID;
		this.name = new AppResourceName(source.Name);
		this.isAnonymousAllowed = source.IsAnonymousAllowed;
		this.resultType = ResourceResultType.values.value(source.ResultType);
    }
}