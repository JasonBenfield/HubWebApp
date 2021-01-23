// Generated code
import { NumericValue } from 'XtiShared/NumericValue';
import { NumericValues } from 'XtiShared/NumericValues';

export class AppEventSeveritys extends NumericValues<AppEventSeverity> {
	constructor(
		public readonly NotSet: AppEventSeverity,
		public readonly CriticalError: AppEventSeverity,
		public readonly AccessDenied: AppEventSeverity,
		public readonly AppError: AppEventSeverity,
		public readonly ValidationFailed: AppEventSeverity
	) {
		super([NotSet,CriticalError,AccessDenied,AppError,ValidationFailed]);
	}
}

export class AppEventSeverity extends NumericValue implements IAppEventSeverity {
	public static readonly values = new AppEventSeveritys(
		new AppEventSeverity(0, 'Not Set'),
		new AppEventSeverity(100, 'Critical Error'),
		new AppEventSeverity(80, 'Access Denied'),
		new AppEventSeverity(70, 'App Error'),
		new AppEventSeverity(60, 'Validation Failed')
	);
	
	private constructor(Value: number, DisplayText: string) {
		super(Value, DisplayText);
	}
}