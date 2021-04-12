# CreditApplications

REST service for credit applications. Returns a decision and interest rate.

- The service accepts a credit amount, the term (repayment in months) and current pre-existing credit amount
- Validation checks for necessary inputs and non-negative values
- Unit tests
- No logging or security implemented

## Rules for loan approval

The service returns a decision and an interest rate percentage, based on the following rules:

| Applied amount  | Decision	|
|-----------------|-------------|
| <2000			  | No			|
| >2000			  | Yes			|
| >69000		  | No			|
###### NOTE: Fixed to include all possible amounts. Original task left range of values out.

## Interest Rate

|Total future debt	| Interest rate% |
|-------------------|----------------|
|  Up to 20000		|		3		 |
|  Up to 40000		|		4		 |
|  Up to 60000		|		5		 |
|  Over 60000		|		6		 |

--------------
Request Body:  
**{
	"CurrentAmount": 8000.5,
	"RequestedAmount": 2000,
  "RepaymentTerm": 11
}**

Response:  
**{
	"Approved": True,
	"InterestRate": 3
}**
