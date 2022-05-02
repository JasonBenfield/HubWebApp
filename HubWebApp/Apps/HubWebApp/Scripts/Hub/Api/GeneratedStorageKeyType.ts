// Generated code
import { NumericValue } from '@jasonbenfield/sharedwebapp/NumericValue';
import { NumericValues } from '@jasonbenfield/sharedwebapp/NumericValues';

export class GeneratedStorageKeyTypes extends NumericValues<GeneratedStorageKeyType> {
	constructor(
		public readonly Guid: GeneratedStorageKeyType,
		public readonly SixDigit: GeneratedStorageKeyType,
		public readonly TenDigit: GeneratedStorageKeyType
	) {
		super([Guid,SixDigit,TenDigit]);
	}
}

export class GeneratedStorageKeyType extends NumericValue implements IGeneratedStorageKeyType {
	public static readonly values = new GeneratedStorageKeyTypes(
		new GeneratedStorageKeyType(0, 'Guid'),
		new GeneratedStorageKeyType(6, 'SixDigit'),
		new GeneratedStorageKeyType(10, 'TenDigit')
	);
	
	private constructor(Value: number, DisplayText: string) {
		super(Value, DisplayText);
	}
}