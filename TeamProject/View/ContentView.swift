//
//  ContentView.swift
//  TeamProject
//
//  Created by Grzegorz Gumieniak on 05/12/2021.
//

import SwiftUI

struct ContentView: View {
    var body: some View {
        ScrollView {
            ForEach(1..<11) { number in
                CategoryView(budgetType: "\(number)")
            }
        }
    }
}

struct ContentView_Previews: PreviewProvider {
    static var previews: some View {
        ContentView()
    }
}
