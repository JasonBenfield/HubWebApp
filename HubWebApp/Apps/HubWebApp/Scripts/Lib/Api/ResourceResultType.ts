// Generated code
import { NumericValue } from '@jasonbenfield/sharedwebapp/NumericValue';
import { NumericValues } from '@jasonbenfield/sharedwebapp/NumericValues';

export class ResourceResultTypes extends NumericValues<ResourceResultType> {
	constructor(
		public readonly None: ResourceResultType,
		public readonly View: ResourceResultType,
		public readonly PartialView: ResourceResultType,
		public readonly Redirect: ResourceResultType,
		public readonly Json: ResourceResultType,
		public readonly File: ResourceResultType,
		public readonly Content: ResourceResultType,
		public readonly Query: ResourceResultType,
		public readonly QueryToExcel: ResourceResultType
	) {
		super([None,View,PartialView,Redirect,Json,File,Content,Query,QueryToExcel]);
	}
}

export class ResourceResultType extends NumericValue implements IResourceResultType {
	public static readonly values = new ResourceResultTypes(
		new ResourceResultType(0, 'None'),
		new ResourceResultType(1, 'View'),
		new ResourceResultType(2, 'Partial View'),
		new ResourceResultType(3, 'Redirect'),
		new ResourceResultType(4, 'Json'),
		new ResourceResultType(5, 'File'),
		new ResourceResultType(6, 'Content'),
		new ResourceResultType(7, 'Query'),
		new ResourceResultType(8, 'Query To Excel')
	);
	
	private constructor(Value: number, DisplayText: string) {
		super(Value, DisplayText);
	}
}