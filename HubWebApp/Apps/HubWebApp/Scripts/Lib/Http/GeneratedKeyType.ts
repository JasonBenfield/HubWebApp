// Generated code
import { NumericValue } from '@jasonbenfield/sharedwebapp/NumericValue';
import { NumericValues } from '@jasonbenfield/sharedwebapp/NumericValues';

export class GeneratedKeyTypes extends NumericValues<GeneratedKeyType> {
	constructor(
		public readonly Guid: GeneratedKeyType,
		public readonly SixDigit: GeneratedKeyType,
		public readonly TenDigit: GeneratedKeyType,
		public readonly Fixed: GeneratedKeyType
	) {
		super([Guid,SixDigit,TenDigit,Fixed]);
	}
}

export class GeneratedKeyType extends NumericValue implements IGeneratedKeyType {
	public static readonly values = new GeneratedKeyTypes(
		new GeneratedKeyType(0, 'Guid'),
		new GeneratedKeyType(6, 'Six Digit'),
		new GeneratedKeyType(10, 'Ten Digit'),
		new GeneratedKeyType(999, 'Fixed')
	);
	
	private constructor(Value: number, DisplayText: string) {
		super(Value, DisplayText);
	}
	
	equalsAny: (...other: this[] | IGeneratedKeyType[] | number[] | string[]) => boolean;
	
	equals: (other: this | IGeneratedKeyType | number | string) => boolean;
}