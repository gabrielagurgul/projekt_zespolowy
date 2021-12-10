//
//  ContentView.swift
//  TeamProject
//
//  Created by Grzegorz Gumieniak on 05/12/2021.
//

import SwiftUI

struct ContentView: View {
	
	@EnvironmentObject var viewModel: BudgetViewModel
	
	let columns = [
		GridItem(.flexible()),
		GridItem(.flexible())
	]
	var body: some View {
		ScrollView {
			LazyVGrid(columns: columns, spacing: 48){
				ForEach(BudgetType.arrayOfBudgetTypes) { budgetType in
					CategoryView(budgetType: budgetType)
				}
			}
		}
	}
}

struct ContentView_Previews: PreviewProvider {
	static var previews: some View {
		ContentView()
			.environmentObject(BudgetViewModel(fetcher: BudgetFetcherImpl()))
		ContentView().preferredColorScheme(.dark)
			.environmentObject(BudgetViewModel(fetcher: BudgetFetcherImpl()))
	}
}
