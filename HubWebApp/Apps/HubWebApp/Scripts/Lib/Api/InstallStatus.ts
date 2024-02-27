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
		new InstallStatus(0, 'Not Set'),
		new InstallStatus(10, 'Install Pending'),
		new InstallStatus(20, 'Install Started'),
		new InstallStatus(30, 'Installed'),
		new InstallStatus(40, 'Delete Pending'),
		new InstallStatus(50, 'Delete Started'),
		new InstallStatus(60, 'Deleted')
	);
	
	private constructor(Value: number, DisplayText: string) {
		super(Value, DisplayText);
	}
}