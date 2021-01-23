// Generated code
import { NumericValue } from 'XtiShared/NumericValue';
import { NumericValues } from 'XtiShared/NumericValues';

export class AppVersionTypes extends NumericValues<AppVersionType> {
	constructor(
		public readonly NotSet: AppVersionType,
		public readonly Major: AppVersionType,
		public readonly Minor: AppVersionType,
		public readonly Patch: AppVersionType
	) {
		super([NotSet,Major,Minor,Patch]);
	}
}

export class AppVersionType extends NumericValue implements IAppVersionType {
	public static readonly values = new AppVersionTypes(
		new AppVersionType(0, 'Not Set'),
		new AppVersionType(1, 'Major'),
		new AppVersionType(2, 'Minor'),
		new AppVersionType(3, 'Patch')
	);
	
	private constructor(Value: number, DisplayText: string) {
		super(Value, DisplayText);
	}
}