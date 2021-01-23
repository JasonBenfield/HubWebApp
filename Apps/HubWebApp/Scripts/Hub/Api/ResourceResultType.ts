// Generated code
import { NumericValue } from 'XtiShared/NumericValue';
import { NumericValues } from 'XtiShared/NumericValues';

export class ResourceResultTypes extends NumericValues<ResourceResultType> {
	constructor(
		public readonly None: ResourceResultType,
		public readonly View: ResourceResultType,
		public readonly PartialView: ResourceResultType,
		public readonly Redirect: ResourceResultType,
		public readonly Json: ResourceResultType
	) {
		super([None,View,PartialView,Redirect,Json]);
	}
}

export class ResourceResultType extends NumericValue implements IResourceResultType {
	public static readonly values = new ResourceResultTypes(
		new ResourceResultType(0, 'None'),
		new ResourceResultType(1, 'View'),
		new ResourceResultType(2, 'PartialView'),
		new ResourceResultType(3, 'Redirect'),
		new ResourceResultType(4, 'Json')
	);
	
	private constructor(Value: number, DisplayText: string) {
		super(Value, DisplayText);
	}
}