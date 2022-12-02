// Generated code
import { NumericValue } from '@jasonbenfield/sharedwebapp/NumericValue';
import { NumericValues } from '@jasonbenfield/sharedwebapp/NumericValues';

export class InstallStatuss extends NumericValues<InstallStatus> {
	constructor(
		public readonly NotSet: InstallStatus,
		public readonly InstallPending: InstallStatus,
		public readonly InstallStarted: InstallStatus,
		public readonly Installed: InstallStatus,
		public readonly DeletePending: InstallStatus,
		public readonly DeleteStarted: InstallStatus,
		public readonly Deleted: InstallStatus
	) {
		super([NotSet,InstallPending,InstallStarted,Installed,DeletePending,DeleteStarted,Deleted]);
	}
}

export class InstallStatus extends NumericValue implements IInstallStatus {
	public static readonly values = new InstallStatuss(
		new InstallStatus(0, 'NotSet'),
		new InstallStatus(10, 'InstallPending'),
		new InstallStatus(20, 'InstallStarted'),
		new InstallStatus(30, 'Installed'),
		new InstallStatus(40, 'DeletePending'),
		new InstallStatus(50, 'DeleteStarted'),
		new InstallStatus(60, 'Deleted')
	);
	
	private constructor(Value: number, DisplayText: string) {
		super(Value, DisplayText);
	}
}