// Generated code
import { NumericValue } from 'XtiShared/NumericValue';
import { NumericValues } from 'XtiShared/NumericValues';

export class AppVersionStatuss extends NumericValues<AppVersionStatus> {
	constructor(
		public readonly NotSet: AppVersionStatus,
		public readonly New: AppVersionStatus,
		public readonly Publishing: AppVersionStatus,
		public readonly Old: AppVersionStatus,
		public readonly Current: AppVersionStatus
	) {
		super([NotSet,New,Publishing,Old,Current]);
	}
}

export class AppVersionStatus extends NumericValue implements IAppVersionStatus {
	public static readonly values = new AppVersionStatuss(
		new AppVersionStatus(0, 'Not Set'),
		new AppVersionStatus(1, 'New'),
		new AppVersionStatus(2, 'Publishing'),
		new AppVersionStatus(3, 'Old'),
		new AppVersionStatus(4, 'Current')
	);
	
	private constructor(Value: number, DisplayText: string) {
		super(Value, DisplayText);
	}
}