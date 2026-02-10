// Generated code
import { NumericValue } from '@jasonbenfield/sharedwebapp/NumericValue';
import { NumericValues } from '@jasonbenfield/sharedwebapp/NumericValues';

export class AppCommandStatuss extends NumericValues<AppCommandStatus> {
	constructor(
		public readonly NotSet: AppCommandStatus,
		public readonly Pending: AppCommandStatus,
		public readonly Started: AppCommandStatus,
		public readonly Completed: AppCommandStatus,
		public readonly Failed: AppCommandStatus,
		public readonly Cancelled: AppCommandStatus
	) {
		super([NotSet,Pending,Started,Completed,Failed,Cancelled]);
	}
}

export class AppCommandStatus extends NumericValue implements IAppCommandStatus {
	public static readonly values = new AppCommandStatuss(
		new AppCommandStatus(0, 'Not Set'),
		new AppCommandStatus(1, 'Pending'),
		new AppCommandStatus(5, 'Started'),
		new AppCommandStatus(10, 'Completed'),
		new AppCommandStatus(99, 'Failed'),
		new AppCommandStatus(999, 'Cancelled')
	);
	
	private constructor(Value: number, DisplayText: string) {
		super(Value, DisplayText);
	}
	
	equalsAny: (...other: this[] | IAppCommandStatus[] | number[] | string[]) => boolean;
	
	equals: (other: this | IAppCommandStatus | number | string) => boolean;
}